using System;

namespace IdentityServer.Webapi.Dtos
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTimeOffset DateOfSubscribe { get; set; }
    }
}
