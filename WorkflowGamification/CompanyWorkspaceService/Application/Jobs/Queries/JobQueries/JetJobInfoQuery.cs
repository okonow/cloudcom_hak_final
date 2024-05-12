using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.JobModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.Queries.JobQueries
{
    public record JetJobInfoQuery : IRequest<JobMainInfoVM>
    {
        public required Guid JobId { internal get; set; }
    }

    internal class JetJobInfoByIdQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper) : IRequestHandler<JetJobInfoQuery, JobMainInfoVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<JobMainInfoVM> Handle(JetJobInfoQuery request, CancellationToken cancellationToken)
        {
            var job = await _applicationDbContext.Jobs
                .Where(j => j.Id == request.JobId)
                .FirstOrDefaultAsync()
                ?? throw new NullEntityException(nameof(Job));

            return _mapper.Map<JobMainInfoVM>(job);
        }
    }
}
