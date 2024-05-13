using Application.StoreAccounts.Commands;
using Application.Wallets.Commands;
using Contracts.ApplyJobContracts;
using Contracts.CreateUserContracts;
using MassTransit;
using MediatR;

namespace WalletService.Consumers
{
    public class SendMoneyToOtherUserConsumer(ISender sender) : IConsumer<SendToMoneyToUserSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<SendToMoneyToUserSagaCommand> context)
        {
            try
            {
                var message = context.Message;
                await _sender.Send(new SendMoneyToOtherWalletCommand 
                {
                    SourceUserId = message.SenderId,
                    DestinationUserId = message.SenderId,
                    MoneyAmount = message.MoneyAmount
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
