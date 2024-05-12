using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands.PatchCommands
{
    public record AddEmployeeToJobCommand : IRequest
    {
        public Guid EmployeeId { internal get; set; }

        public Guid JobId { internal get; set; }
    }

    internal record AddEmployeeToJobCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<AddEmployeeToJobCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(AddEmployeeToJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Include(j => j.JobMetadata)
                .Where(j => j.Id == request.JobId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));

            job.JobMetadata.WorkerId = request.EmployeeId;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
