namespace Contracts.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string? FristName { get; set; }

        public string? MiddleName { get; set; }

        public string? LatName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

    }
}
