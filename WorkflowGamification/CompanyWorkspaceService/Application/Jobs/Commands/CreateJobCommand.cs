using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Commands
{
    public record CreateJobCommand : IRequest<Guid>
    {
        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime Deadline { get; set; }

        public Complexity Complexity { get; set; }

        public required Guid CreatorId { get; set; }

        public required Guid WorkerId { get; set; }
    }

    internal class CreateJobCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Where(j => j.Title == request.Title)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (job != null)
                throw new ExistEntityException(request.Title);

            Job newJob = new()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                JobMetadata = new(request.Deadline, request.Complexity, request.CreatorId, request.WorkerId)
            };

            await _applicationDbContext.Jobs.AddAsync(newJob, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newJob.Id;
        }
    }
}
