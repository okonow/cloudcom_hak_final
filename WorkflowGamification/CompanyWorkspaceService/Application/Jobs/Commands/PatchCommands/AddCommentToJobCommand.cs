using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Jobs.Commands.PatchCommands
{
    public record AddCommentToJobCommand : IRequest
    {
        public required Guid JobId { internal get; set; }

        public required string Comment { internal get; set; }
    }

    internal class AddCommentToJobCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<AddCommentToJobCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(AddCommentToJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Where(j => j.Id == request.JobId)
                .Include(j => j.JobAnswers)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));

            var answer = job.JobAnswers.LastOrDefault();
            job.AddCommentToResponse(answer!.Description, answer.AttachedFile, request.Comment, answer!.DepartureTime);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
