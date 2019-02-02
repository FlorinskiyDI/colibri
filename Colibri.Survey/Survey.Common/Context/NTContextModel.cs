using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.Common.Context
{
    public class NTContextModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string IdentityUrl { get; set; }
        public string IdentityUserToken { get; set; }
    }
}
