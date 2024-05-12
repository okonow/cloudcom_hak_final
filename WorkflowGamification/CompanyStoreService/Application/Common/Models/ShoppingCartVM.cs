using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models
{
    public class ShoppingCartVM : IMapWith<ShoppingCart>
    {
        public IList<Offer>? Offers{ get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ShoppingCart, ShoppingCartVM>();
        }
    }
}
