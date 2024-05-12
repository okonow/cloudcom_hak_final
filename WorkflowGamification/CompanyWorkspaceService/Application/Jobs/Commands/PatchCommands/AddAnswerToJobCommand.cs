using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands.UpdateCommands
{
    public record AddAnswerToJobCommand : IRequest
    {
        public required Guid JobId { get; set; }

        public string? Description { get; set; }

        // TODO: fix problem with file saving
        //public StoredFile? AttachedFile { get; set; }

        public DateTime DepartureTime { get; set; }
    }

    internal class AddAnswerFromJobCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<AddAnswerToJobCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(AddAnswerToJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Where(j => j.Id == request.JobId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));

            job.JobAnswers.Add(new(request.Description, new(), string.Empty, request.DepartureTime));
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
