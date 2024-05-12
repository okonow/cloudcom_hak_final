namespace Contracts.ApplyJobContracts
{
    public record FinishJobSagaCommand
    {
        public Guid JobId { get; set; }
    }

    public record SendToMoneyToUserSagaCommand
    {
        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public decimal MoneyAmount { get; set; }
    }
}
