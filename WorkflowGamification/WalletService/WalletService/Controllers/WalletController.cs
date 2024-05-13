using Application.Common.Models;
using Application.StoreAccounts.Commands;
using Application.StoreAccounts.Queries;
using Application.Wallets.Commands;
using CompanyWorkspaceService.Controllers;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WalletService.Controllers
{
    [ApiController]
    public class WalletController(ISender sender) : BaseController(sender)
    {
        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPost]
        public async Task DepositMoneyIntoTheWalletAsync([FromBody] DepositMoneyIntoTheWalletCommand command)
            => await _sender.Send(command);

        [Authorize]
        [HttpGet("{id}")]
        public async Task<WalletVM> GetInformationWalletBalanceAsync([FromRoute] Guid id)
            => await _sender.Send(new GetBalanceOnWalletQuery { UserId = id });

        [Authorize]
        [HttpPatch]
        public async Task SendMoneyToOtherUserAsync([FromBody] SendMoneyToOtherWalletCommand request)
            => await _sender.Send(request);

        [Authorize(Policy = Polices.MustBeAdministrator)]
        [HttpPatch]
        public async Task WithdrawMoneyFromWalletAsync([FromBody] WithdrawMoneyFromWalletCommand request)
            => await _sender.Send(request);
    }
}
