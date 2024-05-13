using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Statistics.Commands
{
    public record DeleteEmployeeStatisticsCommand : IRequest
    {
        public required Guid EmployeeId { internal get; set; }
    }

    internal class DeleteEmployeeStatisticsCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<DeleteEmployeeStatisticsCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteEmployeeStatisticsCommand request, CancellationToken cancellationToken)
        {
            var statistics = await _applicationDbContext.Statistics
                .Where(s => s.EmployeeId == request.EmployeeId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(EmployeeStatistics));

            _applicationDbContext.Statistics.Remove(statistics);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
