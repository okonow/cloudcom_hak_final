namespace Contracts.CreateUserContracts
{
    public record UserCreatedEvent
    {
        public Guid CorrelationId { get; set; }
      
    }
    public record UserNotCreatedEvent
    {
        public IEnumerable<string>? Errors { get; set; }
    }
}
