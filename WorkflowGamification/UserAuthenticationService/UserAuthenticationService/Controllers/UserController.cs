using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAuthenticationService.Common.Constants;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Data;
using UserAuthenticationService.Models.UserModels;

namespace UserAuthenticationService.Controllers
{
    [ApiController]
    public class UserController(
        IUserService userService,
        ITokenService tokenService) : BaseController
    {
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> CreateNewUserAsync([FromBody] CreateUserRequest request)
        {
            var result = await _userService.CreateUserAsync(new ApplicationUser 
            { 
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Login,
                PasswordHash = request.Password
            });

            if (result.Succeeded)
                return Ok(result.UserId);
            else
                return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] AuthorizeAndDeleteUserRequest request)
        {
            var isAuthenticate = await _userService.AuthorizeAsync(request.Login, request.Password);

            if (isAuthenticate.Succeeded)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                    return Unauthorized();

                var token = await _tokenService.CreateAccessTokenAsync(userId);
                return Ok(new { accessToken = token });
            }

            return BadRequest(isAuthenticate.Errors);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserInformationAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var user = (ApplicationUser)await _userService.FindUserAsync(userId);

            return Ok(new UserInfoResponse(user));
        }

        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInformationAsync(string id)
        {
            var user = (ApplicationUser)await _userService.FindUserAsync(id);
            return Ok(new UserInfoResponse(user));
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUserInformationAsync([FromBody] UpdateUserRequest request)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return BadRequest();

            var result = await _userService.UpdateUserAsync(
                userId, request.FirstName, request.MiddleName, request.LastName, request.OldPassword, request.NewPassword);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await _userService.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteUserAsync([FromBody] AuthorizeAndDeleteUserRequest request)
        {
            var result = await _userService.DeleteUserAsync(request.Login, request.Password);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
