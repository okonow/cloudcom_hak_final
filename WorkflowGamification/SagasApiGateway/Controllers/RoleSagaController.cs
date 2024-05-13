using Contracts.AddRoleToUserContracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SagasApiGateway.Controllers
{
    [ApiController]
    public class RoleSagaController(IPublishEndpoint publishEndpoint) : BaseController
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> AddRoleToUserAsync(Guid userId, string role)
        {
            var employeeRole = "employee";
            await _publishEndpoint.Publish<AddRoleToUserSagaCommand>(new { UserId = userId, Role = role });

            if (role.ToLower() == employeeRole)
                await _publishEndpoint.Publish<CreateEmployeeStatisticsSagaCommand>(new { UserId = userId });

            return Ok();
        }
    }
}
