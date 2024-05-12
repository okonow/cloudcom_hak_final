using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingCarts.Commands
{
    public record PurgeOfferFromShoppingCartCommand : IRequest
    {
        public Guid UserId { internal get; set; }

        public Guid OfferId { internal get; set; }
    }

    internal class DeleteOfferFromShoppingCartCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<PurgeOfferFromShoppingCartCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(PurgeOfferFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _applicationDbContext.ShoppingCarts
              .Where(a => a.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(ShoppingCart));

            var offer = await _applicationDbContext.Offers
              .Where(s => s.Id == request.OfferId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(Offer));

            cart.Offers!.Remove(offer);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
