using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Departments.Commands
{
    public record DeleteDepartmentCommand : IRequest
    {
        public required Guid DepartmentId { internal get; set; }
    }

    internal class DeleteDepartmentCommandHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _applicationDbContext.Departments
                .Where(d => d.Id == request.DepartmentId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NullEntityException(nameof(Department));

            _applicationDbContext.Departments.Remove(department);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
