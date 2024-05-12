using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands
{
    public record CreateDepartmentCommand : IRequest<Guid>
    {
        public required string DepartmentName { internal get; set; }

        public string? DepartmentDescription { internal get; set; }

        public Guid DirectorId { internal get; set; }

        public IList<Guid>? DepartmentEmployeesId { internal get; set; }
    }

    internal class CreateDepartmentCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<CreateDepartmentCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
                .Where(d => d.DepartmentName == request.DepartmentName)
                .FirstOrDefaultAsync(cancellationToken);

            if (department != null)
                throw new ExistEntityException(request.DepartmentName);

            Department newDepartment = new()
            {
                DepartmentName = request.DepartmentName,
                DepartmentDescription = request.DepartmentDescription,
                DirectorId = request.DirectorId,
                DepartmentEmployeesId = request.DepartmentEmployeesId,
                Id = Guid.NewGuid()
            };

            newDepartment.DepartmentEmployeesId ??= [];

            await _applicationDbContext.Departments.AddAsync(newDepartment, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newDepartment.Id;
        }
    }
}
