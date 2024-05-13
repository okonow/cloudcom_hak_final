using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands
{
    public record DeleteJobCommand : IRequest
    {
        public required Guid JobId { internal get; set; }
    }

    public class DeleteJobCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<DeleteJobCommand>
    {
        public readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Where(j => j.Id == request.JobId)
                .FirstOrDefaultAsync()
                ?? throw new NullEntityException(nameof(Job));

            _applicationDbContext.Jobs.Remove(job);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
