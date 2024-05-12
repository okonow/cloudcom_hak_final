using Contracts.Models;

namespace Contracts.CreateUserContracts
{
    public record CreateUserSagaCommand
    {
        public required User User { get; set; }
    }
    public record CreateShoppingListSagaCommand
    {
        public required Guid CorrelationId { get; set; }
    }
    public record CreateUserWalletSagaCommand
    {
        public required Guid CorrelationId { get; set; }
    }
}
