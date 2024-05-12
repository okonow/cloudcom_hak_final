using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Common.Models
{
    public class OfferVM : IMapWith<Offer>
    {
        public Guid Id { get; set; }

        public required string OfferName { get; set; }

        public OfferDescription? Description { get; set; }

        public decimal ProductCost { get; set; }

        public int CountOfProducts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Offer, OfferVM>();
        }
    }
}
