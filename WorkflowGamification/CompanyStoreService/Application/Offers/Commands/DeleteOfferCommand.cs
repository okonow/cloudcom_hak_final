using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Commands
{
    public record DeleteOfferCommand : IRequest
    {
        public required Guid OfferId { internal get; set; }
    }

    internal class DeleteProductCommandHandler(
        IApplicationDbContext applicationDbContext)
        : IRequestHandler<DeleteOfferCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var product = await _applicationDbContext.Offers
              .Where(a => a.Id == request.OfferId)
              .FirstOrDefaultAsync(cancellationToken)
              ?? throw new NullEntityException(nameof(Offer));

            _applicationDbContext.Offers.Remove(product);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
