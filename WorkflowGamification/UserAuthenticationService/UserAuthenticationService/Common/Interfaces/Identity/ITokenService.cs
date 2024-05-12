namespace UserAuthenticationService.Common.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<string> CreateAccessTokenAsync(string userId);

        Task<string?> RefreshAccessTokenAsync(string userId);
    }
}
