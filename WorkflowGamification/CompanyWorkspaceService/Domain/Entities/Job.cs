using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Job : BaseEntity
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public required JobMetadata JobMetadata { get; set; }

        public IList<JobAnswer> JobAnswers { get; set; } = [];

        public bool IsFinished { get; set; }

        public void AddCommentToResponse(
            string? description,
            StoredFile? storedFile,
            string? comment,
            DateTime departureTime)
        {

            var response = JobAnswers.FirstOrDefault(r 
                => r.Description == description 
                && r.AttachedFile == storedFile
                && r.Comment == comment
                && r.DepartureTime == departureTime
            );

            if (response != null)
            {
                var updatedResponse = new JobAnswer(description, storedFile, comment, departureTime);
                JobAnswers.Remove(response);
                JobAnswers.Add(updatedResponse);
            }
        }
    }
}
