using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations
{
    public static class SystemRoleScopes
    {
        public const string SUPER_ADMIN = "SuperAdmin";
        public const string ADMIN = "Admin";
        public const string USER = "User";
        public const string GROUP_CREATOR = "GroupCreator";

        public static class Groups
        {
            public const string ADMIN = "GroupAdmin";
            public const string EDITOR = "GroupEditor";
            public const string VIEWER = "GroupViewer";
        }

    }
}
