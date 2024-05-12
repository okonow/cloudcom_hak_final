using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobQueries.DepartmentJobsQueries
{
    public record GetUnfinishedJobsInfoInDepartmentQuery : IRequest<IList<JobMainInfoVM>>
    {
        public required Guid DepartmentId { internal get; set; }
    }

    internal class GetUnfinishedJobsInDepartmentQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetUnfinishedJobsInfoInDepartmentQuery, IList<JobMainInfoVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<JobMainInfoVM>> Handle(GetUnfinishedJobsInfoInDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            var unfinishedJobs = await _applicationDbContext.Jobs
                .Include(j => j.JobMetadata)
                .Where(j => j.JobMetadata.CreatorId == department.DirectorId)
                .ToListAsync()
                ?? throw new NullEntityException(nameof(Job));

            var unfinishedJobsVm = unfinishedJobs.Select((j, JobMainInfoVM) => _mapper.Map<JobMainInfoVM>(j)).ToList();

            return unfinishedJobsVm;
        }
    }
}
