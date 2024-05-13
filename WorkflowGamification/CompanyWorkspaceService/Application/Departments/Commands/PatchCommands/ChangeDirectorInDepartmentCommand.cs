using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands.PatchCommands
{
    public record ChangeDirectorInDepartmentCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }

        public required Guid NewDirectorId { internal get; set; }
    }

    internal class ChangeDirectorInDepartmentCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<ChangeDirectorInDepartmentCommand>
    {
        private IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(ChangeDirectorInDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            department.DirectorId = request.NewDirectorId;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
