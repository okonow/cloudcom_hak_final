using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands.PatchCommands
{
    public record FinishJobCommand : IRequest
    {
        public required Guid JobId { internal get; set; }
    }

    internal class CloseJobCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<FinishJobCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(FinishJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Where(j => j.Id == request.JobId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));

            job.IsFinished = true;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
