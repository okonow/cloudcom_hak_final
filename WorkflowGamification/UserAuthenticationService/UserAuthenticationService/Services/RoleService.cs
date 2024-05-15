using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserAuthenticationService.Common.Constants;
using UserAuthenticationService.Common.Exceptions;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Data;

namespace UserAuthenticationService.Services
{
    public class RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new(roleName));
            return result.Succeeded;
        }

        [Authorize]
        public async Task<string> GetUserRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NullEntityException(nameof(ApplicationUser));

            var roles = await _userManager.GetRolesAsync(user)
                ?? throw new NullEntityException(nameof(Roles));

            return roles[0];
        }

        public async Task<bool> AddRoleToUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NullEntityException(nameof(ApplicationUser));

            var isExist = await _roleManager.RoleExistsAsync(role);

            if (isExist)
            {
                var result = await _userManager.AddToRoleAsync((ApplicationUser)user, role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> ChangeRoleOfUserAsync(string userId, string oldRole, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId)
              ?? throw new NullEntityException(nameof(ApplicationUser));

            var isExistOldRole = await _roleManager.RoleExistsAsync(oldRole);
            var isExistNewRole = await _roleManager.RoleExistsAsync(newRole);

            if (isExistNewRole && isExistOldRole)
            {
                await _userManager.RemoveFromRoleAsync(user, oldRole);
                var result = await _userManager.AddToRoleAsync(user, newRole);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> DeleteRoleFromUserAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId)
              ?? throw new NullEntityException(nameof(ApplicationUser));

            var isExist = await _roleManager.RoleExistsAsync(role);

            if (isExist)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, role);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            var result = await _roleManager.DeleteAsync(new(roleName));
            return result.Succeeded;
        }
    }
}
