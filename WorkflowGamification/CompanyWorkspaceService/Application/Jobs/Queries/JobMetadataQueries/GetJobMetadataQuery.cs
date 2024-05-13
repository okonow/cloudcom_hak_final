using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobMetadataQueries
{
    public record GetJobMetadataQuery : IRequest<JobMetadataVM>
    {
        public required Guid JobId { internal get; set; }
    }

    internal class GetJobMetadataQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetJobMetadataQuery, JobMetadataVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<JobMetadataVM> Handle(GetJobMetadataQuery request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
               .Include(j => j.JobMetadata)
               .Where(j => j.Id == request.JobId)
               .FirstOrDefaultAsync(cancellationToken: cancellationToken)
               ?? throw new NullEntityException(nameof(Job));

            return _mapper.Map<JobMetadataVM>(job.JobMetadata);
        }
    }
}
