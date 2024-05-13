using Application.StoreAccounts.Commands;
using Contracts.CreateUserContracts;
using MassTransit;
using MediatR;

namespace WalletService.Consumers
{
    public class CreateWalletConsumer(ISender sender) : IConsumer<CreateUserWalletSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<CreateUserWalletSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                await _sender.Send(new CreateWalletCommand { UserId = message.CorrelationId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}
