using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.DepartmentModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Queries
{
    public record GetDepartmentQuery : IRequest<DepartmentVM>
    {
        public required Guid DepartmentId { internal get; set; }
    }

    public class GetDepartmentByNameHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetDepartmentQuery, DepartmentVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<DepartmentVM> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            return _mapper.Map<DepartmentVM>(department);
        }
    }
}
