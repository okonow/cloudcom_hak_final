using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands.PatchCommands
{
    public record AddEmployeeInDepartmentCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }

        public required Guid NewEmployeeId { internal get; set; }
    }

    internal class AddEmployeeInDepartmentCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<AddEmployeeInDepartmentCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        public async Task Handle(AddEmployeeInDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            department.DepartmentEmployeesId!.Add(request.NewEmployeeId);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
