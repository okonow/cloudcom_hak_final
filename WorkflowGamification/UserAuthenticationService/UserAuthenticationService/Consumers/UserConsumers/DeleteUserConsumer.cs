using Contracts.DeleteUserContracts;
using MassTransit;
using UserAuthenticationService.Common.Interfaces.Identity;

namespace UserAuthenticationService.Consumers.UserConsumers
{
    public class DeleteUserConsumer(IUserService userService) : IConsumer<DeleteUserSagaCommand>
    {
        private readonly IUserService _userService = userService;

        public async Task Consume(ConsumeContext<DeleteUserSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                var receivedData = message.UserId;

                // TODO: доделать вывод Result вместо bool при вызове метода
                var isDeleted = await _userService.DeleteUserAsync(receivedData.ToString());

                if (isDeleted)
                {
                    await context.RespondAsync<UserDeletedEvent>(new() { CorrelationId = receivedData });
                    await context.Publish(new UserDeletedEvent { CorrelationId = receivedData });
                }
                else
                    await context.RespondAsync<UserNotDeletedEvent>(new() { Errors = ["incorrect password"] });
            }
            catch (Exception ex)
            {
                // TODO: добавить нормальное логгирование
                Console.WriteLine(ex.Message);
                await context.RespondAsync<UserNotDeletedEvent>(new() { Errors = [ex.Message] });
            }
        }
    }
}
