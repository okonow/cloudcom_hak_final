using Microsoft.AspNetCore.Identity;
using UserAuthenticationService.Common.Interfaces;

namespace UserAuthenticationService.Data
{
    public class ApplicationUser : IdentityUser, IUser
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }
    }
}
