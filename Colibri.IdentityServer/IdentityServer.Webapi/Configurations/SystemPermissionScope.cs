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

    public static class SystemPermissionScope
    {
        // GROUP
        public const string GroupListAll = "identity.groups.listAll";
        public const string GroupList = "identity.groups.list";
        public const string GroupGet = "identity.groups.get";
        public const string GroupGetSubgroups = "identity.groups.getSubgroups";
        public const string GroupCreate = "identity.groups.create";
        public const string GroupUpdate = "identity.groups.update";
        public const string GroupDelete = "identity.groups.delete";
        
        // MEMBER

        // USER

        // SYSTEM

        public static List<string> GetValues()
        {
            var values = new List<string>();
            Type t = typeof(SystemPermissionScope);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo fi in fields)
            {
                values.Add(fi.GetValue(null).ToString());
            }
            return values;
        }
    }
    
}
