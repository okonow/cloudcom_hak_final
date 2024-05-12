using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands
{
    public record UpdateOfferCommand : IRequest
    {
        public Guid ProductId { internal get; set; }

        public required string ProductName { internal get; set; }

        public OfferDescription? Description { internal get; set; }

        public decimal ProductCost { internal get; set; }

        public int CountOfProducts { internal get; set; }
    }

    internal class UpdateOfferCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<UpdateOfferCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await _applicationDbContext.Offers
                .Include(p => p.Description)
                .Where(a => a.Id == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NullEntityException(nameof(Offer));

            offer.OfferName = request.ProductName;
            offer.Description = request.Description;
            offer.OfferCost = request.ProductCost;
            offer.CountOfProducts = request.CountOfProducts;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
