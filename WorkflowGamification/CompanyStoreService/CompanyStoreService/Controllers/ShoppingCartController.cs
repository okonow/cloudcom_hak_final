using Application.Common.Models;
using Application.ShoppingCarts.Commands;
using Application.ShoppingCarts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStoreService.Controllers
{
    [ApiController]
    public class ShoppingCartController(ISender sender) : BaseController(sender)
    {
        [HttpPost]
        public async Task<Guid> CreateShoppingCartAsync([FromBody] CreateShoppingCartCommand command)
            => await _sender.Send(command);

        [HttpGet]
        public async Task<IList<OfferVM>> GetOffersFromShoppingCartAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetOffersInShoppingCartQuery() { UserId = userId });

        [HttpPatch]
        public async Task AddNewOfferFromShoppingCartAsync([FromBody] AddOfferToShoppingCartCommand command)
            => await _sender.Send(command);

        [HttpPatch]
        public async Task DeleteOfferFromShoppingCartAsync([FromBody] PurgeOfferFromShoppingCartCommand command)
            => await _sender.Send(command);

        [HttpDelete]
        public async Task DeleteShoppingCartAsync([FromHeader] Guid userId)
            => await _sender.Send(new DeleteShoppingCartCommand() { UserId = userId });
    }
}
