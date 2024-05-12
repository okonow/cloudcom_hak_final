using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingLists.Commands
{
    public record BuyOfferToShoppingListCommand : IRequest
    {
        public required Guid UserId { internal get; set; }

        public required Guid OfferId { internal get; set; }
    }

    internal class BuyOfferToShoppingListCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<BuyOfferToShoppingListCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(BuyOfferToShoppingListCommand request, CancellationToken cancellationToken)
        {
            var list = await _applicationDbContext.ShoppingLists
              .Where(a => a.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(ShoppingList)); 

            var product = await _applicationDbContext.Offers
              .Where(s => s.Id == request.OfferId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(Offer));

            list.Offers!.Add(product);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
