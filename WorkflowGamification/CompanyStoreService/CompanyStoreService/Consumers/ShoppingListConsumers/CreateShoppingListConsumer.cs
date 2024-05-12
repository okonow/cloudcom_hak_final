using Application.ShoppingLists.Commands;
using Contracts.CreateUserContracts;
using Dependencies;
using MassTransit;
using MediatR;

namespace CompanyStoreService.Consumers.ShoppingListConsumers
{
    public class CreateShoppingListConsumer(ISender sender) : IConsumer<CreateShoppingListSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<CreateShoppingListSagaCommand> context)
        {
            try
            {
                await _sender.Send(new CreateShoppingListCommand { UserId = context.Message.CorrelationId });
            }
            catch (Exception ex)
            {
                // TODO: доделать обработку исключений при создании списка купленных товаров
            }
        }
    }
}
