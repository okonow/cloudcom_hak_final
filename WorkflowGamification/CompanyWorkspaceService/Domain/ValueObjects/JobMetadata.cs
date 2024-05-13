using Domain.Common;
using Domain.Enums;

namespace Domain.ValueObjects
{
    public class JobMetadata : ValueObject
    {
        public JobMetadata()
        {           
        }

        public JobMetadata(DateTime deadline, Complexity complexity, Guid creatorId, Guid workerId)
        {
            Deadline = deadline;
            Complexity = complexity;
            CreatorId = creatorId;
            WorkerId = workerId;
        }

        public DateTime Deadline { get; init; } 

        public Complexity Complexity { get; init; }

        public Guid CreatorId { get; set; }

        public Guid? WorkerId { get; set; }
    }
}