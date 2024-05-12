using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StoreAccounts.Commands
{
    public record DeleteWalletCommand : IRequest
    {
        public required Guid UserId { get; set; }
    }

    internal class DeleteWalletCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<DeleteWalletCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
        {
            var account = await _applicationDbContext.Wallets
               .Where(a => a.UserId == request.UserId)
               .FirstOrDefaultAsync(cancellationToken)
               ?? throw new NullEntityException(nameof(Wallet));

            _applicationDbContext.Wallets.Remove(account);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
