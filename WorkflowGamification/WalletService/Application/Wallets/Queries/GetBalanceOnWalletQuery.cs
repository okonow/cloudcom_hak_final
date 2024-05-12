using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.StoreAccounts.Queries
{
    public record GetBalanceOnWalletQuery : IRequest<WalletVM>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class GetBalanceOnWalletHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetBalanceOnWalletQuery, WalletVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<WalletVM> Handle(GetBalanceOnWalletQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _applicationDbContext.Wallets
              .Where(a => a.UserId == request.UserId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(Wallet));

            return _mapper.Map<WalletVM>(wallet);
        }
    }
}
