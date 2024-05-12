using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries
{
    public record GetAllOffersQuery : IRequest<IList<OfferVM>>
    {
    }

    internal class GetAllProductsQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetAllOffersQuery, IList<OfferVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<OfferVM>> Handle(GetAllOffersQuery request, CancellationToken cancellationToken)
        {
            var products = await _applicationDbContext.Offers
                .ToListAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Offer));

            var productsVM = products.Select((p, ProductVM) => _mapper.Map<OfferVM>(p)).ToList();
            return productsVM;
        }
    }
}
