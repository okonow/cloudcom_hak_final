using UserAuthenticationService.Common.Interfaces;
using UserAuthenticationService.Data;

namespace UserAuthenticationService.Models.UserModels
{
    public class UserInfoResponse(ApplicationUser user)
    {
        public string? FirstName { get; } = user.FirstName;

        public string? MiddleName { get; } = user.MiddleName;

        public string? LastName { get; } = user.LastName;

        public string? Login { get; } = user.Email;
    }
}
