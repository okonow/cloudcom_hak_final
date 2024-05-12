namespace WorkflowGamificationWebApp.Server.Dtos
{
    public class UserDto
    {
        public string? FristName { get; set; }

        public string? MiddleName { get; set; }

        public string? LatName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}