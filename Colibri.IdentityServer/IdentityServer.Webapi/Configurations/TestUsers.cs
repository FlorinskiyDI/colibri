using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations
{
    public class TestUsers
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "SuperAdmin",
                    Password = "SuperAdmin",

                    Claims = new List<Claim>
                    {
                        //new Claim( "role", EnumRoles.SuperAdmin.ToString() )
                    }
                }
            };
        }
    }
}
