using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthenticationService.Common.Exceptions;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Data;

namespace UserAuthenticationService.Services
{
    public class TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public async Task<string> CreateAccessTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NullEntityException($"{nameof(ApplicationUser)}");

            var userRole = await _userManager.GetRolesAsync(user);
            var accessToken = GenerateAccessToken(userId, userRole);


            //await _userManager.UpdateAsync(user);

            if (accessToken == null)
                throw new InvalidOperationException();

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        public async Task<string?> RefreshAccessTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId)
                ?? throw new NullEntityException(nameof(ApplicationUser));
            var userRoles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken newToken = GenerateAccessToken(userId, userRoles);

            return new JwtSecurityTokenHandler().WriteToken(newToken);
        }

        private JwtSecurityToken GenerateAccessToken(string userId, IList<string> roles)
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
                authClaims.Add(new(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var expiresTimeString = _configuration["JWT:AccessTokenExpireInMinutes"];

            if (!int.TryParse(expiresTimeString, out int expiresTime))
                throw new InvalidOperationException("Time must be number");

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                expires: DateTime.UtcNow.AddMinutes(expiresTime),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
