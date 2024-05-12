using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands.PurgeCommands
{
    public record PurgeEmployeeFromDepartmentCommand : IRequest
    {
        public Guid DepartmentId { internal get; set; }

        public Guid EmployeeId { internal get; set; }
    }

    internal class PurgeEmployeeFromDepartmentCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<PurgeEmployeeFromDepartmentCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(PurgeEmployeeFromDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            var isDeleted = department.DepartmentEmployeesId!.Remove(request.EmployeeId);

            if (!isDeleted)
                throw new InvalidOperationException("Cannot delete user");

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
