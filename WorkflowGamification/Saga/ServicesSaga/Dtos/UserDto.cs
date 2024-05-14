namespace WorkflowGamificationWebApp.Server.Dtos
{
    public class UserDto
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}