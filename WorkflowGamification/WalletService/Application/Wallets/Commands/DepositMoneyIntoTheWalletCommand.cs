using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StoreAccounts.Commands
{
    public record DepositMoneyIntoTheWalletCommand : IRequest
    {
        public required Guid UserId { internal get; set; }

        public decimal MoneyAmount { internal get; set; }
    }

    internal class DepositMoneyIntoTheWalletCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<DepositMoneyIntoTheWalletCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DepositMoneyIntoTheWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _applicationDbContext.Wallets
              .Where(a => a.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(Wallet));

            wallet.AddMoney(request.MoneyAmount);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
