using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Offer : BaseEntity
    {
        public required string OfferName { get; set; }

        public OfferDescription? Description { get; set; } = null!;

        public decimal OfferCost { get; set; }

        public int CountOfProducts { get; set; }
    }
}
