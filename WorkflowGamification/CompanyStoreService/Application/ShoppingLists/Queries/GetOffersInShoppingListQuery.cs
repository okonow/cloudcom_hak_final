using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ShoppingLists.Queries
{
    public record GetOffersInShoppingListQuery : IRequest<IList<OfferVM>>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class GetOffersInShoppingListHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetOffersInShoppingListQuery, IList<OfferVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<OfferVM>> Handle(GetOffersInShoppingListQuery request, CancellationToken cancellationToken)
        {
            var list = await _applicationDbContext.ShoppingLists
                .Include(l => l.Offers)
                .Where(l => l.UserId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NullEntityException(nameof(ShoppingList));

            var productsVM = list.Offers!.Select((p, ProductVM) => _mapper.Map<OfferVM>(p)).ToList();
            return productsVM;
        }
    }
}
