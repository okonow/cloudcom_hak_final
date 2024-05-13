using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobQueries.UsersJobsQueries
{
    public record GetUserJobsInfoQuery : IRequest<IList<JobMainInfoVM>>
    {
        public required Guid UserId { get; set; }
    }

    internal class GetUserJobsQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetUserJobsInfoQuery, IList<JobMainInfoVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<JobMainInfoVM>> Handle(GetUserJobsInfoQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _applicationDbContext.Jobs
                .Include(j => j.JobMetadata)
                .Where(j => j.JobMetadata.WorkerId == request.UserId || j.JobMetadata.CreatorId == request.UserId)
                .ToListAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));

            var jobsVm = jobs.Select((j, JobVM) => _mapper.Map<JobMainInfoVM>(j)).ToList();
            return jobsVm;
        }
    }
}
