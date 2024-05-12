using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands
{
    public record UpdateDepartmentCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }

        public string? DepartmentName { internal get; set; }

        public string? DepartmentDescription { internal get; set; }

        public Guid DirectorId { internal get; set; }

        public IList<Guid>? DepartmentEmployeesId { internal get; set; }
    }

    internal class UpdateDepartmentCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            if (request.DepartmentName != null)
                department.DepartmentName = request.DepartmentName;

            department.DepartmentDescription = request.DepartmentDescription;
            department.DirectorId = request.DirectorId;

            request.DepartmentEmployeesId ??= [];

            department.DepartmentEmployeesId = request.DepartmentEmployeesId;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
