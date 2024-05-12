using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Offer> Offers { get; }

        DbSet<ShoppingCart> ShoppingCarts { get; }

        DbSet<ShoppingList> ShoppingLists { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
