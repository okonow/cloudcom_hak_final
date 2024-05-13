using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands.PurgeCommands
{
    public record PurgeEmployeesFromCompanyCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }
    }

    internal class PurgeEmployeesFromCompanyCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<PurgeEmployeesFromCompanyCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(PurgeEmployeesFromCompanyCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
               .Where(d => d.Id == request.DepartmentId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Department));

            department.DepartmentEmployeesId!.Clear();
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
