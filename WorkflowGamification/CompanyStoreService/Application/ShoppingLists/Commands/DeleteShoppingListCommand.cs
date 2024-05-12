using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingLists.Commands
{
    public record DeleteShoppingListCommand : IRequest
    {
        public required Guid UserId { internal get; set; }
    }

    internal class DeleteShoppingListCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<DeleteShoppingListCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var list = await _applicationDbContext.ShoppingLists
                .Include(l => l.Offers)
              .Where(l => l.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(ShoppingList));

            _applicationDbContext.ShoppingLists.Remove(list);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
