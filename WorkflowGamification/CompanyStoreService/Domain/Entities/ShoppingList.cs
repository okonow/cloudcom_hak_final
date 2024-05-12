using Domain.Common;

namespace Domain.Entities
{
    public class ShoppingList : BaseEntity
    {
        public IList<Offer>? Offers { get; set; } = [];
    }
}
