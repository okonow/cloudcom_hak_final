using Application.StoreAccounts.Commands;
using Contracts.DeleteUserContracts;
using MassTransit;
using MediatR;

namespace WalletService.Consumers
{
    public class DeleteWalletConsumer(ISender sender) : IConsumer<DeleteWalletSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<DeleteWalletSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                await _sender.Send(new DeleteWalletCommand { UserId = message.UserId});
            }
            catch (Exception ex)
            {

            }
        }
    }
}
