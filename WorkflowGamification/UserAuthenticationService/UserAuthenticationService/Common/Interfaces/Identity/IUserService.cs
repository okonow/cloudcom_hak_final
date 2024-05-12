using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UserAuthenticationService.Models;

namespace UserAuthenticationService.Common.Interfaces.Identity
{
    public interface IUserService
    {
        Task<IUser> FindUserAsync(string userId);

        Task<Result> CreateUserAsync(IUser newUser);

        Task<Result> AuthorizeAsync(string login, string password);

        Task<bool> IsAuthorizedAsync(ClaimsPrincipal claims, string refreshToken, AuthorizationPolicy policy);

        Task<bool> UpdateUserAsync(string userId, string firstName, string middleName, string lastName, string oldPassword, string newPassword);

        Task SignOutAsync();

        Task<bool> DeleteUserAsync(string useId);

        Task<bool> DeleteUserAsync(string email, string password);
    }
}
