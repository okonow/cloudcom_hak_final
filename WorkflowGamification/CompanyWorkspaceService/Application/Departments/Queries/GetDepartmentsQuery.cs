using Application.Common.Interfaces;
using Application.Common.Models.DepartmentModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Queries
{
    public record GetDepartmentsQuery : IRequest<IList<DepartmentVM>>
    {
    }

    internal class GetDepartmentsQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetDepartmentsQuery, IList<DepartmentVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<DepartmentVM>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _applicationDbContext.Departments.ToListAsync(cancellationToken: cancellationToken);
            var departmentVMs = departments.Select((d, DepartmentVM) => _mapper.Map<DepartmentVM>(d)).ToList();

            return departmentVMs;
        }
    }
}
