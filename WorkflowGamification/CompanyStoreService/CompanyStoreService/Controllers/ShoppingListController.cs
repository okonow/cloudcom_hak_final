using Application.Common.Models;
using Application.ShoppingLists.Commands;
using Application.ShoppingLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStoreService.Controllers
{
    [ApiController]
    public class ShoppingListController(ISender sender) : BaseController(sender)
    {
        [HttpPost]
        public async Task<Guid> CreateShoppingListAsync([FromBody] CreateShoppingListCommand command)
            => await _sender.Send(command);

        [HttpGet]
        public async Task<IList<OfferVM>> GetOffersFromShoppingCartAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetOffersInShoppingListQuery() { UserId = userId });

        [HttpPatch]
        public async Task AddNewOfferFromShoppingCartAsync([FromBody] BuyOfferToShoppingListCommand command)
            => await _sender.Send(command);

        [HttpDelete]
        public async Task DeleteShoppingCartAsync([FromHeader] Guid userId)
            => await _sender.Send(new DeleteShoppingListCommand() { UserId = userId });
    }
}
