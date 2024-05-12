using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Statistics.Queries
{
    public record GetEmployeeStatisticsQuery : IRequest<EmployeeStatisticsVM>
    {
        public required Guid EmployeeId { internal get; set; }
    }

    internal class GetEmployeeStatisticsQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetEmployeeStatisticsQuery, EmployeeStatisticsVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<EmployeeStatisticsVM> Handle(GetEmployeeStatisticsQuery request, CancellationToken cancellationToken)
        {
            var statistics = await _applicationDbContext.Statistics
                .Where(s => s.EmployeeId == request.EmployeeId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(EmployeeStatistics));

            return _mapper.Map<EmployeeStatisticsVM>(statistics);
        }
    }
}
