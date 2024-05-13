using Domain.Common;

namespace Domain.ValueObjects
{
    public class JobAnswer :ValueObject
    {
        public JobAnswer()
        {
            
        }

        public JobAnswer(
            string? description,
            StoredFile? storedFile,
            string? comment,
            DateTime departureTime)
        {
            Description = description;
            AttachedFile = storedFile;
            Comment = comment;
            DepartureTime = departureTime;
        }

        public string? Description { get; init; }

        public StoredFile? AttachedFile { get; init; }

        public string? Comment { get; init; }

        public DateTime DepartureTime { get; init; }
    }
}