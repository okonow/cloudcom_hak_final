using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingCarts.Commands
{
    public record DeleteShoppingCartCommand : IRequest
    {
        public required Guid UserId { internal get; set; }
    }

    internal class DeleteShoppingCartCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<DeleteShoppingCartCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _applicationDbContext.ShoppingCarts
                .Include(c => c.Offers)
              .Where(c => c.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(ShoppingCart));

            _applicationDbContext.ShoppingCarts.Remove(cart);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
