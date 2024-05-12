using Domain.Common;

namespace Domain.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public IList<Offer>? Offers { get; set; } = [];
    }
}
