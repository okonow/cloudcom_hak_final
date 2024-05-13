using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Statistics.Commands
{
    public record CreateEmployeeStatisticsCommand : IRequest<Guid>
    {
        public required Guid EmployeeId { internal get; set; }
    }

    internal class CreateEmployeeStatisticsCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<CreateEmployeeStatisticsCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateEmployeeStatisticsCommand request, CancellationToken cancellationToken)
        {
            var statistics = await _applicationDbContext.Statistics
                .Where(s => s.EmployeeId == request.EmployeeId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (statistics != null)
                throw new ExistEntityException(nameof(EmployeeStatistics));

            EmployeeStatistics newStatistics = new()
            {
                Id = Guid.NewGuid(),
                EmployeeId = request.EmployeeId
            };

            await _applicationDbContext.Statistics.AddAsync(newStatistics, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newStatistics.Id;
        }
    }
}
