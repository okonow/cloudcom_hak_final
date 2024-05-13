using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAuthenticationService.Common.Constants;
using UserAuthenticationService.Common.Interfaces.Identity;

namespace UserAuthenticationService.Controllers
{
    [ApiController]
    public class TokenController(ITokenService tokenService) : BaseController
    {
        private readonly ITokenService _tokenService = tokenService;

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var newToken = await _tokenService.RefreshAccessTokenAsync(userId);
            return Ok(newToken);
        }
    }
}
