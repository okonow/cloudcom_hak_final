using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models
{
    public class ShoppingListVM : IMapWith<ShoppingList>
    {
        public IList<Offer>? Offers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingList, ShoppingListVM>();
        }
    }
}
