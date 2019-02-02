using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Dtos.Users;
using IdentityServer.Webapi.Dtos.Views;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer.Webapi.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;
        protected readonly IUowProvider _uowProvider;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IEmailSenderService emailSenderService,
            IUowProvider uowProvider,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSenderService = emailSenderService;
            _uowProvider = uowProvider;
        }

        public async Task<SearchResult<AppUserPageDto>> GetSearchData(SearchQuery searchEntry)
        {
            // generate sort expression
            var sort = searchEntry.OrderStatement == null
                ? new OrderBy<ApplicationUser>(c => c.OrderBy(d => d.UserName))
                : new OrderBy<ApplicationUser>(searchEntry.OrderStatement.ColumName, searchEntry.OrderStatement.Reverse);
            // generate filter expression
            Filter<ApplicationUser> filters = null;
            var searchResult = new SearchResult<AppUserPageDto>();

            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<ApplicationUser>();
                var startRow = searchEntry.SearchQueryPage?.PageNumber;
                var pageLength = searchEntry.SearchQueryPage?.PageLength;

                try
                {
                    var data = await repository.QueryPageAsync(filters?.Expression, null, null, startRow, pageLength);
                    searchResult = new SearchResult<AppUserPageDto>()
                    {
                        ItemList = data.Select(c => new AppUserPageDto
                        {
                            Id = c.Id.ToString(),
                            UserName = c.UserName,
                            Email = c.Email,
                            EmailConfirmed = c.EmailConfirmed,
                            EmailConfirmInvitationDate = c.EmailConfirmInvitationDate,
                            EmailConfirmTokenLifespan = c.EmailConfirmTokenLifespan
                        }).ToList()
                    };

                    if (searchEntry.SearchQueryPage != null)
                    {
                        var totalCount = await repository.CountAsync();
                        searchResult.SearchResultPage = new SearchResultPage()
                        {
                            TotalItemCount = totalCount,
                            PageLength = searchEntry.SearchQueryPage.PageLength,
                            PageNumber = totalCount / searchEntry.SearchQueryPage.PageLength
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return searchResult;
        }



        public async Task<ApplicationUser> AddUserByEmailWithoutPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var emailConfirmTokenLifespan = Double.Parse(_configuration["TokenProviders:EmailConfirmTokenProvider:TokenLifespan"]);
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmInvitationDate = DateTimeOffset.UtcNow
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new ArgumentException("The app user was not created");
                }
                await _userManager.AddToRoleAsync(user, SystemRoleScopes.USER); // set default role 
                await SendInvitationByEmailConfirmationToken(user.Id.ToString()); // send invite to user
            }

            return user;
        }

        public async Task SendInvitationByEmailConfirmationToken(string userId)
        {
            var emailTokenLifespan = Double.Parse(_configuration["TokenProviders:EmailConfirmTokenProvider:TokenLifespan"]);
            using (var uow = _uowProvider.CreateUnitOfWork())
            {
                var repository = uow.GetRepository<ApplicationUser>();
                // get user and update emailConfirm data 
                var entity = await repository.GetAsync(userId);
                entity.EmailConfirmInvitationDate = DateTimeOffset.UtcNow;
                entity.EmailConfirmTokenLifespan = emailTokenLifespan;
                repository.Update(entity);
                uow.SaveChanges();

                // send invitation using email confirm token
                var confirmationToken = await GetEmailConfirmationToken(entity.Email);
                string codeHtmlVersion = HttpUtility.UrlEncode(confirmationToken);
                var confirmationUrl = $@"http://localhost:5050/Account/RegisterByEmail/?userId={ entity.Id }&code={ codeHtmlVersion }";
                await _emailSenderService.SendAccountConfirmationEmailAsync(null, entity.Email, "Confirm your account", confirmationUrl);
            }
        }

        private async Task<string> GetEmailConfirmationToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return token;
        }

        public async Task<UserFullDetailsViewModel> GetUserFullDetails(string userId)
        {
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    var repository = uow.GetCustomRepository<IAppUserRepository>();
                    var result = await repository.GetUserFullDetails(userId);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task SetPolicy(UserPolicyDto userPolicy)
        {
            foreach (var email in userPolicy.Emails)
            {
                // get user data
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = await AddUserByEmailWithoutPassword(email);
                }

                await _userManager.AddToRolesAsync(user, userPolicy.Roles);
            }
        }
    }
}
