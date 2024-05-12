using Application.Common.Models;
using Application.Offers.Queries;
using Application.Products.Commands;
using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStoreService.Controllers
{
    [ApiController]
    public class OfferController(ISender sender) : BaseController(sender)
    {
        [HttpPost]
        public async Task<Guid> CreateNewOfferAsync([FromBody] CreateOfferCommand command)
            => await _sender.Send(command);

        [HttpGet]
        public async Task<OfferVM> GetOfferAsync([FromHeader] Guid offerId)
            => await _sender.Send(new GetOfferQuery() { OfferId = offerId });

        [HttpGet]
        public async Task<IList<OfferVM>> GetUserOffersAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetUserOffersQuery { UserId = userId });

        [HttpGet]
        public async Task<IList<OfferVM>> GetOffersAsync()
            => await _sender.Send(new GetAllOffersQuery());

        [HttpPut]
        public async Task UpdateOfferAsync([FromBody] UpdateOfferCommand command)
            => await _sender.Send(command);

        [HttpDelete]
        public async Task DeleteOfferAsync([FromHeader] Guid offerId)
            => await _sender.Send(new DeleteOfferCommand() { OfferId = offerId });
    }
}
