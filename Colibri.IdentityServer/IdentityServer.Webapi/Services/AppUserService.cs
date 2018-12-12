using dataaccesscore.Abstractions.Uow;
using dataaccesscore.EFCore.Query;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Search;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer.Webapi.Services
{
    public class AppUserService : IAppUserService
    {
        private IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderService _emailSenderService;
        protected readonly IUowProvider _uowProvider;

        public AppUserService(
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
                    searchResult = new SearchResult<AppUserPageDto>() {
                        ItemList = data.Select(c => new AppUserPageDto
                        {
                            Id = c.Id,
                            UserName = c.UserName,
                            Email = c.Email,
                            EmailConfirmed = c.EmailConfirmed
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
                user = new ApplicationUser {
                    UserName = email,
                    Email = email
                };
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    throw new ArgumentException("The app user was not created");
                }
                // send invite to user
                var confirmationToken = await GetEmailConfirmationToken(user);
                string codeHtmlVersion = HttpUtility.UrlEncode(confirmationToken);
                var confirmationUrl = $@"http://localhost:5050/Account/RegisterByEmail/?userId={ user.Id }&code={ codeHtmlVersion }";
                await _emailSenderService.SendAccountConfirmationEmailAsync(null, email, "Confirm your account", confirmationUrl);
            }
            //
            return user;
        }

        public async Task<string> GetEmailConfirmationToken(ApplicationUser model)
        {
            _userManager.
                    EmailConfirmTokenLifespan = TimeSpan.FromMilliseconds(emailConfirmTokenLifespan)
            //EmailConfirmInvitationDate = DateTimeOffset.UtcNow
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //
            return token;
        }

    }
}
