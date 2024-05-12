using Application.Statistics.Commands;
using Contracts.AddRoleToUserContracts;
using MassTransit;
using MediatR;

namespace CompanyWorkspaceService.Consumers.ShoppingListConsumers
{
    public class CreateEmployeeStatisticsConsumer(ISender sender) : IConsumer<CreateEmployeeStatisticsSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<CreateEmployeeStatisticsSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                await _sender.Send(new CreateEmployeeStatisticsCommand { EmployeeId = message.UserId });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
