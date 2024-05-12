using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationService.Common.Constants;
using UserAuthenticationService.Common.Interfaces.Identity;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> AddRoleToUserAsync(string id, [FromHeader] string role)
        {
           var result =  await _rolesService.AddRoleToUserAsync(id, role);

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ChangeUserRoleAsync(string id, [FromHeader] string oldRole, [FromHeader] string newRole)
        {
            var result = await _rolesService.ChangeRoleOfUserAsync(id, oldRole, newRole);

            if (result)
                return Ok();

            return BadRequest();

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> AddNewRoleToUserAsync(string id, [FromHeader] string role)
        {
            var result = await _rolesService.AddRoleToUserAsync(id, role);

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
