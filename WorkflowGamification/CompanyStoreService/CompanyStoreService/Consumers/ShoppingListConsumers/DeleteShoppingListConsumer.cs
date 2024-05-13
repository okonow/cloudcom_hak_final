using Application.ShoppingLists.Commands;
using Contracts.DeleteUserContracts;
using MassTransit;
using MediatR;

namespace CompanyStoreService.Consumers.ShoppingListConsumers
{
    public class DeleteShoppingListConsumer(ISender sender) : IConsumer<DeleteShoppingListSagaCommand>
    {
        private readonly ISender _sender = sender;

        // UNDONE
        public async Task Consume(ConsumeContext<DeleteShoppingListSagaCommand> context)
        {
            try
            {
                await _sender.Send(new CreateShoppingListCommand { UserId = context.Message.UserId });
            }
            catch (Exception ex)
            {
                // TODO: доделать обработку исключений при создании списка купленных товаров
                Console.WriteLine(ex.Message);
            }
        }
    }
}
