using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IEmployeesStatisticsSorter
    {
        public IList<EmployeeStatisticsVM> SortByDescending(IList<EmployeeStatisticsVM> employees);
    }
}
