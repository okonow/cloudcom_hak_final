using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobAnswersQueries
{
    public record GetJobAnswersQuery : IRequest<IList<JobAnswerVM>>
    {
        public required Guid JobId { internal get; set; }
    }

    internal class GetJobAnswerQueryCommand(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetJobAnswersQuery, IList<JobAnswerVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<JobAnswerVM>> Handle(GetJobAnswersQuery request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Include(j => j.JobAnswers)
                .Where(j => j.Id == request.JobId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));
            
            var answers = job.JobAnswers
                .Select((a, JobAnswerVM) => _mapper.Map<JobAnswerVM>(a))
                .ToList();

            return answers;
        }
    }
}
