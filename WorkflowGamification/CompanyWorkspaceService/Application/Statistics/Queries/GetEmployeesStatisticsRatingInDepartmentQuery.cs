using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Statistics.Queries
{
    public record GetEmployeesStatisticsRatingInDepartmentQuery : IRequest<IList<EmployeeStatisticsVM>>
    {
        public required Guid DepartmentId { internal get; set; }
    }

    internal class GetEmployeesStatisticsRatingInDepartmentQueryHandler(
        IApplicationDbContext applicationDbContext,
        IEmployeesStatisticsSorter employeesStatisticsSorter,
        IMapper mapper)
        : IRequestHandler<GetEmployeesStatisticsRatingInDepartmentQuery, IList<EmployeeStatisticsVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IEmployeesStatisticsSorter _employeesStatisticsSorter = employeesStatisticsSorter;

        public async Task<IList<EmployeeStatisticsVM>> Handle(GetEmployeesStatisticsRatingInDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
                .Include(d => d.DepartmentEmployeesId)
                .Where(d => d.Id == request.DepartmentId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NullEntityException(nameof(Department));

            var statistics = await _applicationDbContext.Statistics
                .Where(s => department.DepartmentEmployeesId!.Contains(s.EmployeeId))
                .ToListAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(EmployeeStatistics));

            var statisticsVM = statistics.Select((s, EmployeeStatisticsVM) => _mapper.Map<EmployeeStatisticsVM>(s)).ToList();
            statisticsVM = (List<EmployeeStatisticsVM>)_employeesStatisticsSorter.SortByDescending(statisticsVM);

            return statisticsVM;
        }
    }
}
