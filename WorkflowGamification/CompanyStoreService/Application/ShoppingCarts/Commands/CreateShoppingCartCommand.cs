using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingCarts.Commands
{
    public record CreateShoppingCartCommand : IRequest<Guid>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class CreateShoppingCartCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<CreateShoppingCartCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _applicationDbContext.ShoppingCarts
               .Where(a => a.UserId == request.UserId)
               .FirstOrDefaultAsync(cancellationToken);
            if (cart != null)
                throw new ExistEntityException(nameof(ShoppingCart));

            ShoppingCart newCart = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId
            };

            await _applicationDbContext.ShoppingCarts.AddAsync(newCart, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newCart.Id;
        }
    }
}
