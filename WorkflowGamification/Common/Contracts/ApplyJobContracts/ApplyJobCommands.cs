namespace Contracts.ApplyJobContracts
{
    public record FinishJobSagaCommand
    {
        public Guid DirectorId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid JobId { get; set; }

        public decimal MoneyAmount { get; set; }
    }

    public record SendToMoneyToUserSagaCommand
    {
        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public decimal MoneyAmount { get; set; }
    }
}
