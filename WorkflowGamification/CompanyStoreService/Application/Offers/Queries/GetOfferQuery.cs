using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries
{
    public record GetOfferQuery : IRequest<OfferVM>
    {
        public required Guid OfferId { internal get; set; }
    }

    internal class GetProductQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
        : IRequestHandler<GetOfferQuery, OfferVM>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<OfferVM> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            var product = await _applicationDbContext.Offers
                .Include(p => p.Description)
                .Where(a => a.Id == request.OfferId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NullEntityException(nameof(Offer));

            return _mapper.Map<OfferVM>(product);
        }
    }
}
