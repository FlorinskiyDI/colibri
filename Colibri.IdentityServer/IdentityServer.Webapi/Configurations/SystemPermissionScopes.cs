using System;
using System.Collections.Generic;
using System.Reflection;

namespace IdentityServer.Webapi.Configurations
{
    // region PERMISSION ACTIONS //

    // Create
    // Update
    // UpdateConfig
    // UpdateUserRole
    // Delete
    // List
    // ListAll
    // ListActive
    // FullView
    // Get
    // Manage
    // Manipulate
    // Reject
    // Export
    // Import
    // Cancel
    // RemoveFromOrganization // Удалить из организации
    // VerifyImage
    // ListAvailableFeatures

    // endregion

    public class SystemStaticPermissions
    {
        public class Configs
        {
            public const string ConfigsGet = "identity.configs.get";
            public const string ConfigsUpdate = "identity.configs.update";
        }

        public class Users
        {
            public const string SetSuperAdmin = "identity.users.setSuperAdmin";
            public const string List = "identity.users.list";
            public const string Get = "identity.users.get";
            public const string Invite = "identity.users.invite";
            public const string Update = "identity.users.update";
            public const string Disable = "identity.users.disable";
            public const string GetIamPolicy = "identity.users.getIamPolicy";
        }

        public class Groups
        {
            public const string Create = "identity.groups.create";
            public const string Delete = "identity.groups.delete";
            public const string List = "identity.groups.list";
            public const string ListAll = "identity.groups.listAll";
            public const string ListRoot = "identity.groups.listRoot";
            public const string Get = "identity.groups.get";
            public const string Update = "identity.groups.update";
            public const string GetSubgroups = "identity.groups.getSubgroups";
        }

    }
}
