using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands
{
    public record CreateOfferCommand : IRequest<Guid>
    {
        public required Guid CreatorId { internal get; set; }

        public required string ProductName { internal get; set; }

        public OfferDescription? Description { internal get; set; }

        public decimal ProductCost { internal get; set; }

        public int CountOfProducts { internal get; set; } = int.MaxValue;
    }

    internal class CreateProductCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<CreateOfferCommand, Guid>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            Offer newOffer = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.CreatorId,
                Description = request.Description,
                OfferCost = request.ProductCost,
                OfferName = request.ProductName
            };

            await _applicationDbContext.Offers.AddAsync(newOffer, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return newOffer.Id;
        }
    }
}
