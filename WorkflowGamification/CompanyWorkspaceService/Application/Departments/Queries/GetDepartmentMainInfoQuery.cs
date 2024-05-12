using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.DepartmentModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Queries
{
    public record GetDepartmentMainInfoQuery : IRequest<DepartmentMainInfoVM>
    {
        public Guid DepartmentId { internal get; set; }
    }

    internal class GetDepartmentMainInfoQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetDepartmentMainInfoQuery, DepartmentMainInfoVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<DepartmentMainInfoVM> Handle(GetDepartmentMainInfoQuery request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            return _mapper.Map<DepartmentMainInfoVM>(department);
        }
    }
}
