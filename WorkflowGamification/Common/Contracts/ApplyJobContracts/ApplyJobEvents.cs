namespace Contracts.ApplyJobContracts
{
    public record JobFinishedEvent
    {
        public required Guid DirectorId { get; set; }
    }

    public record JobNotFinishedEvent
    {
        public required IEnumerable<string> Errors { get; set; }
    }
}
