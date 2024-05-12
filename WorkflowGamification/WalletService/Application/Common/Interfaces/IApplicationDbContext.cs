using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Wallet> Wallets { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
