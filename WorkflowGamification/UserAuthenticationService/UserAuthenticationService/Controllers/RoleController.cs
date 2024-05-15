using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAuthenticationService.Common.Constants;
using UserAuthenticationService.Common.Interfaces.Identity;
using UserAuthenticationService.Models.RoleModels;

namespace UserAuthenticationService.Controllers
{
    [Authorize(Policy = Polices.MustBeAdministrator)]
    [ApiController]
    public class RoleController(IRoleService rolesService) : BaseController
    {
        private readonly IRoleService _rolesService = rolesService;

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] string roleName)
        {
            var result = await _rolesService.CreateRoleAsync(roleName);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoleAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = await _rolesService.GetUserRoleAsync(userId);
            return Ok(new { Role = role });
        }


        [HttpPut]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] UserRoleRequest request)
        {
           var result =  await _rolesService.AddRoleToUserAsync(request.UserId.ToString(), request.RoleName);

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserRoleAsync([FromHeader] string id, [FromHeader] string oldRole, [FromHeader] string newRole)
        {
            var result = await _rolesService.ChangeRoleOfUserAsync(id, oldRole, newRole);

            if (result)
                return Ok();

            return BadRequest();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoleAsync([FromHeader] string roleName)
        {
            var result = await _rolesService.DeleteRoleAsync(roleName);

            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
