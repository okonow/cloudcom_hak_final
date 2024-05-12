using Domain.Common;

namespace Domain.Entities
{
    public class EmployeeStatistics : BaseEntity
    {
        public required Guid EmployeeId { get; set; }

        public int CompletedEasyJobsCount { get; set; }

        public int CompletedNormalJobsCount { get; set; }

        public int CompletedDifficultJobsCount { get; set; }

        public TimeSpan AverageTimeForCompletingJob { get; set; }
    }
}
