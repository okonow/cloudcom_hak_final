using Application.Common.Models;
using Application.ShoppingLists.Commands;
using Application.ShoppingLists.Queries;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStoreService.Controllers
{
    [ApiController]
    public class ShoppingListController(ISender sender) : BaseController(sender)
    {
        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPost]
        public async Task<Guid> CreateShoppingListAsync([FromBody] CreateShoppingListCommand command)
            => await _sender.Send(command);

        [Authorize(Policy = Polices.MustBeInRole)]
        [HttpGet]
        public async Task<IList<OfferVM>> GetOffersFromShoppingCartAsync([FromHeader] Guid userId)
            => await _sender.Send(new GetOffersInShoppingListQuery() { UserId = userId });

        [Authorize(Policy = Polices.MustBeInRole)]
        [HttpPatch]
        public async Task AddNewOfferFromShoppingCartAsync([FromBody] BuyOfferToShoppingListCommand command)
            => await _sender.Send(command);

        [Authorize(Policy = Polices.MustBeInRole)]
        [HttpDelete]
        public async Task DeleteShoppingCartAsync([FromHeader] Guid userId)
            => await _sender.Send(new DeleteShoppingListCommand() { UserId = userId });
    }
}
