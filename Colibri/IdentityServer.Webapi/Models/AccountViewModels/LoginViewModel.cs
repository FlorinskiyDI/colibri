using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Models.AccountViewModels
{
    public class LoginViewModel : LoginInputModel
    {
        public bool EnableLocalLogin { get; set; }
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
    }

    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }
    }
}
