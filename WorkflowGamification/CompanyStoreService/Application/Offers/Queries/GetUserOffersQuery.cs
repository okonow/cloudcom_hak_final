using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Offers.Queries
{
    public record GetUserOffersQuery : IRequest<IList<OfferVM>>
    {
        public required Guid UserId { internal get; set; }
    }

    internal class GetUserOffersQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetUserOffersQuery, IList<OfferVM>>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IList<OfferVM>> Handle(GetUserOffersQuery request, CancellationToken cancellationToken)
        {
            var products = await _applicationDbContext.Offers
                .Where(o => o.UserId == request.UserId)
                .ToListAsync(cancellationToken: cancellationToken)
                ?? throw new NullEntityException(nameof(Offer));

            var productsVM = products.Select((p, ProductVM) => _mapper.Map<OfferVM>(p)).ToList();
            return productsVM;
        }
    }
}
