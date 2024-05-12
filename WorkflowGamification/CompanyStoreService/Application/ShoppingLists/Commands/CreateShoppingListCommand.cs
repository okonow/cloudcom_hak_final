using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingLists.Commands
{
    public record CreateShoppingListCommand : IRequest<Guid>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class CreateShoppingListCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<CreateShoppingListCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var list = await _applicationDbContext.ShoppingLists
               .Where(a => a.UserId == request.UserId)
               .FirstOrDefaultAsync(cancellationToken);
            if (list != null)
                throw new ExistEntityException(nameof(ShoppingList));

            ShoppingList newList = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId
            };

            await _applicationDbContext.ShoppingLists.AddAsync(newList, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newList.Id;
        }
    }
}
