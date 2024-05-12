using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Wallets.Commands
{
    public record WithdrawMoneyFromWalletCommand : IRequest
    {
        public required Guid UserId { internal get; set; }

        public decimal MoneyAmount { internal get; set; }
    }

    internal class WithdrawMoneyFromWalletCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<WithdrawMoneyFromWalletCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(WithdrawMoneyFromWalletCommand request, CancellationToken cancellationToken)
        {
            var account = await _applicationDbContext.Wallets
               .Where(a => a.UserId == request.UserId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Wallet));

            account.WithdrawMoney(request.MoneyAmount);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
