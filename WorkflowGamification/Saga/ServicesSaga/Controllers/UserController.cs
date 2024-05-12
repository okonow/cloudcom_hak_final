using Contracts.CreateUserContracts;
using Contracts.DeleteUserContracts;
using Contracts.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowGamificationWebApp.Server.Dtos;

namespace ServicesSaga.Controllers
{
    [ApiController]
    public class UserController(IBusControl busControl) : BaseController
    {
        private readonly IBusControl _busControl = busControl;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FristName = userDto.FristName,
                MiddleName = userDto.MiddleName,
                LatName = userDto.LatName,
                Email = userDto.Email,
                Password = userDto.Password
            };
            var requestClient = _busControl.CreateRequestClient<CreateUserSagaCommand>();
            var (created, notCreated) 
                = await requestClient.GetResponse<UserCreatedEvent, UserNotCreatedEvent>(new CreateUserSagaCommand { User = user });

            if (created.IsCompletedSuccessfully)
            {
                var response = await created;
                return Ok(response.Message.CorrelationId);
            }
            else
            {
                var response = await notCreated;
                return BadRequest(response.Message.Errors);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync([FromHeader] Guid userId, string password)
        {
            var requestClient = _busControl.CreateRequestClient<DeleteUserSagaCommand>();
            var (deleted, notDeleted)
                = await requestClient.GetResponse<UserDeletedEvent, UserNotDeletedEvent>(new DeleteUserSagaCommand { UserId = userId, Password = password });

            if (deleted.IsCompletedSuccessfully)
            {
                var response = await deleted;
                return Ok(response.Message.CorrelationId);
            }
            else
            {
                var response = await notDeleted;
                return BadRequest(response.Message.Errors);
            }
        }
    }
}
