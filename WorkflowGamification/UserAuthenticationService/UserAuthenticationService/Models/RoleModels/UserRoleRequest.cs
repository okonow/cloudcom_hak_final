namespace UserAuthenticationService.Models.RoleModels
{
    public class UserRoleRequest
    {
        public  Guid  UserId { get; set; }

        public string RoleName { get; set; }
    }
}
