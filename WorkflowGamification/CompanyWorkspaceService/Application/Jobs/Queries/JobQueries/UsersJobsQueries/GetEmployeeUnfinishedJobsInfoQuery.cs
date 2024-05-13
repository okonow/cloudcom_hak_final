using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobQueries.UsersJobsQueries
{
    public record GetEmployeeUnfinishedJobsInfoQuery : IRequest<IList<JobMainInfoVM>>
    {
        public required Guid EmployeeId { internal get; set; }
    }

    internal class GetEmployeeUnfinishedJobsQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetEmployeeUnfinishedJobsInfoQuery, IList<JobMainInfoVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<JobMainInfoVM>> Handle(GetEmployeeUnfinishedJobsInfoQuery request, CancellationToken cancellationToken)
        {
            var unfinishedJobs = await _applicationDbContext.Jobs
                .Include(j => j.JobMetadata)
                .Where(j => j.JobMetadata.WorkerId == request.EmployeeId && !j.IsFinished)
                .ToListAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Job));

            var unfinishedJobsVm = unfinishedJobs.Select((j, JobMainInfoVM) => _mapper.Map<JobMainInfoVM>(j)).ToList();
            return unfinishedJobsVm;
        }
    }
}
