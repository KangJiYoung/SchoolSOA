using Microsoft.AspNetCore.Identity;

namespace SchoolSOA.Services.Identity.Entities
{
    public class AuthUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}