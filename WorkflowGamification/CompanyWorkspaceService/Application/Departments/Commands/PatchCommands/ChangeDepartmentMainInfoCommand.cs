using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands.PatchCommands
{
    public record ChangeDepartmentMainInfoCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }

        public required string DepartmentName { internal get; set; }

        public string? Description { internal get; set; }
    }

    internal class UpdateDepartmentMainInfoCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<ChangeDepartmentMainInfoCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(ChangeDepartmentMainInfoCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            department.DepartmentName = request.DepartmentName;
            department.DepartmentDescription = request.Description;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
