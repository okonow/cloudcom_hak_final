using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands.PurgeCommands
{
    public record PurgeDirectorFromDepartmentCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }
    }

    internal class PurgeDirectorFromDepartmentCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<PurgeDirectorFromDepartmentCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        public async Task Handle(PurgeDirectorFromDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            department.DirectorId = Guid.Empty;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
