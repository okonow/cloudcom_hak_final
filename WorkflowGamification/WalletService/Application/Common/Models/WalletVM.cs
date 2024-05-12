using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models
{
    public class WalletVM : IMapWith<Wallet>
    {
        public decimal MoneyBalance { get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Wallet, WalletVM>();
        }
    }
}
