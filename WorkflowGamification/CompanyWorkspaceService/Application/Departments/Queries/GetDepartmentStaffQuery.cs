using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.DepartmentModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Queries
{
    public record GetDepartmentStaffQuery : IRequest<DepartmentStaffVM>
    {
        public required Guid DepartmentId { get; set; }
    }

    internal class GetDepartmentStaffQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetDepartmentStaffQuery, DepartmentStaffVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<DepartmentStaffVM> Handle(GetDepartmentStaffQuery request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            return _mapper.Map<DepartmentStaffVM>(department);
        }
    }
}
