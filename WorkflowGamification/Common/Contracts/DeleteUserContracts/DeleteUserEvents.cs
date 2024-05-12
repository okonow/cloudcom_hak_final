namespace Contracts.DeleteUserContracts
{
    public record UserDeletedEvent
    {
        public Guid CorrelationId { get; set; }
    }

    public record UserNotDeletedEvent
    {
        public IEnumerable<string>? Errors { get; set; }
    }
}
