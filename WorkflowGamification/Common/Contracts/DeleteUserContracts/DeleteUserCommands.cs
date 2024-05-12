namespace Contracts.DeleteUserContracts
{
    public record DeleteUserSagaCommand
    {
        public required Guid UserId { get; set; }
        public required string Password { get; set; }
    }

    public record DeleteShoppingListSagaCommand
    {
        public required Guid UserId { get; set; }
    }

    public record DeleteWalletSagaCommand
    {
        public required Guid UserId { get; set; }
    }
}
