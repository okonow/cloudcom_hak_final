using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StoreAccounts.Commands
{
    public record CreateWalletCommand : IRequest<Guid>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class CreateStoreAccountCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<CreateWalletCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _applicationDbContext.Wallets
                .Where(a => a.UserId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);
            if (wallet != null)
                throw new ExistEntityException(nameof(WalletVM));

            Wallet newWallet = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId
            };

            await _applicationDbContext.Wallets.AddAsync(newWallet, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newWallet.Id;
        }
    }
}
