using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingCarts.Commands
{
    public record AddOfferToShoppingCartCommand : IRequest
    {
        public required Guid UserId { internal get; set; }

        public required Guid OfferId { internal get; set; }
    }

    internal class AddOfferToShoppingCartCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<AddOfferToShoppingCartCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(AddOfferToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _applicationDbContext.ShoppingCarts
              .Where(a => a.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(ShoppingCart));

            var product = await _applicationDbContext.Offers
              .Where(s => s.Id == request.OfferId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(Offer));

            cart.Offers!.Add(product);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
