using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingCarts.Queries
{
    public record GetOffersInShoppingCartQuery : IRequest<IList<OfferVM>>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class GetOffersInShoppingCartHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetOffersInShoppingCartQuery, IList<OfferVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<OfferVM>> Handle(GetOffersInShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _applicationDbContext.ShoppingCarts
                .Include(c => c.Offers)
                .Where(c => c.UserId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NullEntityException(nameof(ShoppingCart));

            var productsVM = cart.Offers!.Select((p, ProductVM) => _mapper.Map<OfferVM>(p)).ToList();
            return productsVM;
        }
    }
}
