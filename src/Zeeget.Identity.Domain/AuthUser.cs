using Microsoft.AspNetCore.Identity;

namespace Zeeget.Identity.Domain
{
    public class AuthUser : IdentityUser
    {
        public DateTime LastLoginDate { get; set; }
    }
}
