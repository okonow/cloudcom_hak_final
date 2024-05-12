namespace UserAuthenticationService.Models.UserModels
{
    public class AuthorizeAndDeleteUserRequest
    {
        public required string Login { get; set; }

        public required string Password { get; set; }
    }
}
