using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations.AspNetIdentity
{
    [DataContract(Name = "CustomTenantResourceType", Namespace = "")]
    public sealed class CustomTenantResourceType
    {
        [DataMember]
        public string Tenant { get; set; }
        [DataMember]
        public string Value { get; set; }

        // Constructors
        public CustomTenantResourceType()
        {
        }

        public CustomTenantResourceType(string tenant, string value)
        {
            Tenant = tenant;
            Value = value;
        }        
    }
}
