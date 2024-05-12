using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Wallets.Commands
{
    public record SendMoneyToOtherWalletCommand : IRequest
    {
        public required Guid SourceUserId { internal get; set; }

        public required Guid DestinationUserId { internal get; set; }

        public decimal MoneyAmount { internal get; set; }
    }

    internal class SendMoneyToOtherWalletCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<SendMoneyToOtherWalletCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(SendMoneyToOtherWalletCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _applicationDbContext.Wallets
               .Where(a => a.UserId == request.SourceUserId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Wallet));

            var destinationAccount = await _applicationDbContext.Wallets
               .Where(a => a.UserId == request.DestinationUserId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Wallet));

            sourceAccount.WithdrawMoney(request.MoneyAmount);
            destinationAccount.AddMoney(request.MoneyAmount);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
