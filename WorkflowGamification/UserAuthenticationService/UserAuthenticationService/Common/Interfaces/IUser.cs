namespace UserAuthenticationService.Common.Interfaces
{
    public interface IUser
    {
        public string Id { get; }

        public string? FirstName { get; }

        public string? LastName { get; }

        public string? RefreshToken { get; }

        public DateTime RefreshTokenExpiry { get; }
    }
}
