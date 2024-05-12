using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UserAuthenticationService.Common.Exceptions;
using UserAuthenticationService.Common.Interfaces;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Data;
using UserAuthenticationService.Models;

namespace UserAuthenticationService.Services
{
    public class UserService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthorizationService authorizationService) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public async Task<Result> CreateUserAsync(IUser newUser)
        {
            var email = ((ApplicationUser)newUser).Email
                ?? throw new InvalidOperationException();

            if (await _userManager.FindByEmailAsync(email) != null)
                throw new ExistEntityException(email);

            var user = (ApplicationUser)newUser;
            user.UserName = user.Email;

            var res = await _userManager.CreateAsync(user, user.PasswordHash 
                ?? throw new InvalidOperationException());

            //await _signInManager.SignInAsync(user, true);

            return ResultExtensions.ToApplicationResult(res, Guid.Parse(user.Id));
        }

        public async Task<Result> AuthorizeAsync(string login, string password)
        {
            var user = await _userManager.FindByEmailAsync(login)
                ?? throw new NullEntityException(nameof(ApplicationUser));

            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            await _signInManager.SignInAsync(user, true);
            return ResultExtensions.ToApplicationResult(Guid.Parse(user.Id), result.Succeeded);
        }

        public async Task<bool> IsAuthorizedAsync(ClaimsPrincipal claims, string refreshToken, AuthorizationPolicy policy)
        {
            var id = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (id == null)
                return false;

            var user = await _userManager.FindByIdAsync(id.Value);
            var hasValidPolicy = await _authorizationService.AuthorizeAsync(claims, policy);

            if (user is null || user.RefreshTokenExpiry < TimeProvider.System.GetUtcNow()
                || user.RefreshToken != refreshToken || !hasValidPolicy.Succeeded)
            {
                await SignOutAsync();
                return false;
            }

            return true;
        }

        public async Task<IUser> FindUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
               ?? throw new NullEntityException(nameof(ApplicationUser));

            return user;
        }

        public async Task<bool> UpdateUserAsync(
            string userId,
            string firstName,
            string middleName,
            string lastName,
            string oldPassword,
            string newPassword)
        {
            var user = (ApplicationUser)await FindUserAsync(userId);

            user.FirstName = firstName;
            user.MiddleName = middleName;
            user.LastName = lastName;

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<bool> DeleteUserAsync(string useId)
        {
            var user = (ApplicationUser)await FindUserAsync(useId);
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
        public async Task<bool> DeleteUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new NullEntityException($"{nameof(ApplicationUser)}");

            var canDelete = await _userManager.CheckPasswordAsync(user, password);

            if (canDelete)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }

            return false;
        }
    }
}
