using Application.Common.Interfaces;
using Application.Common.Models;

namespace Infrastructure.Helpers
{
    public class EmployeeStatisticsSorter : IEmployeesStatisticsSorter
    {
        public IList<EmployeeStatisticsVM> SortByDescending(IList<EmployeeStatisticsVM> employees)
        {
            employees = [.. employees.OrderBy(e => e.AverageTimeForCompletingJob)
                .OrderBy(e => e.CompletedDifficultJobsCount)
                .OrderBy(e => e.CompletedEasyJobsCount)
                .OrderBy(e => e.CompletedNormalJobsCount)];

            return employees;
        }
    }
}
