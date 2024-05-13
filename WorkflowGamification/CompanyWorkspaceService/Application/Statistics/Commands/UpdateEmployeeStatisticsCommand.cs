using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Statistics.Commands
{
    public record UpdateEmployeeStatisticsCommand : IRequest
    {
        public required Guid EmployeeId { internal get; set; }

        public int CompletedEasyJobsCount { internal get; set; }

        public int CompletedNormalJobsCount { internal get; set; }

        public int CompletedDifficultJobsCount { internal get; set; }

        public TimeSpan AverageTimeForCompletingJob { internal get; set; }
    }

    internal class UpdateEmployeeStatisticsCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<UpdateEmployeeStatisticsCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(UpdateEmployeeStatisticsCommand request, CancellationToken cancellationToken)
        {
            var statistics = await _applicationDbContext.Statistics
                .Where(s => s.EmployeeId == request.EmployeeId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(EmployeeStatistics));

            statistics.CompletedDifficultJobsCount = request.CompletedDifficultJobsCount;
            statistics.CompletedNormalJobsCount = request.CompletedNormalJobsCount;
            statistics.CompletedEasyJobsCount = request.CompletedEasyJobsCount;
            statistics.AverageTimeForCompletingJob = request.AverageTimeForCompletingJob;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
