using Contracts.AddRoleToUserContracts;
using MassTransit;
using UserAuthenticationService.Common.Interfaces.Identity;

namespace UserAuthenticationService.Consumers.RoleConsumers
{
    public class AddRoleToUserConsumer(IRoleService roleService) : IConsumer<AddRoleToUserSagaCommand>
    {
        private readonly IRoleService _roleService = roleService;
        // UNDONE
        public async Task Consume(ConsumeContext<AddRoleToUserSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                await _roleService.AddRoleToUserAsync(message.UserId.ToString(), message.Role);
            }
            catch (Exception ex)
            {
                // TODO: доделать обработку ошибок
                // TODO: добавить нормальное логгирование
                Console.WriteLine(ex.Message);
            }
        }
    }
}
