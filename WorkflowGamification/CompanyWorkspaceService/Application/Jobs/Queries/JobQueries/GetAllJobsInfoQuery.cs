using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobQueries
{
    public record GetAllJobsInfoQuery : IRequest<IList<JobMainInfoVM>>
    {
    }

    internal class GetAllJobsQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper) : IRequestHandler<GetAllJobsInfoQuery, IList<JobMainInfoVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<JobMainInfoVM>> Handle(GetAllJobsInfoQuery request, CancellationToken cancellationToken)
        {
            var jobs = await _applicationDbContext.Jobs.ToListAsync(cancellationToken);
            var jobsInfo = jobs.Select((j, JobMainInfoVM) => _mapper.Map<JobMainInfoVM>(j)).ToList();

            return jobsInfo;
        }
    }
}
