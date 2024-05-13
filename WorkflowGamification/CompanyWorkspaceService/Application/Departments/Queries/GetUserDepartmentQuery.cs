using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.DepartmentModels;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Queries
{
    public record GetUserDepartmentQuery : IRequest<DepartmentVM>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class GetUserDepartmentQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetUserDepartmentQuery, DepartmentVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<DepartmentVM> Handle(GetUserDepartmentQuery request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.DirectorId == request.UserId || d.DepartmentEmployeesId!.Contains(request.UserId))
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            return _mapper.Map<DepartmentVM>(department);
        }
    }
}
