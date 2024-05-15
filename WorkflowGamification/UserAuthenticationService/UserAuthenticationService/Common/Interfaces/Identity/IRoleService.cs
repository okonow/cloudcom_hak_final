namespace UserAuthenticationService.Common.Interfaces.Identity
{
    public interface IRoleService
    {

        Task<bool> CreateRoleAsync(string roleName);

        Task<string> GetUserRoleAsync(string userId);

        Task<bool> AddRoleToUserAsync(string userId, string role);

        Task<bool> ChangeRoleOfUserAsync(string userId, string oldRole, string newRole);

        Task<bool> DeleteRoleFromUserAsync(string userId, string role);

        Task<bool> DeleteRoleAsync(string roleName);
    }
}
