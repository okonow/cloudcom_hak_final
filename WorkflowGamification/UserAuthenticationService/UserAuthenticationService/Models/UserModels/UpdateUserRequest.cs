namespace UserAuthenticationService.Models.UserModels
{
    public class UpdateUserRequest
    {
        public required string FirstName { get; set; }

        public required string MiddleName { get; set; }

        public required string LastName { get; set; }

        public required string OldPassword { get; set; }

        public required string NewPassword { get; set; }


    }
}
