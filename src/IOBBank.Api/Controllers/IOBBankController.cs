using IOBBank.Application.Commands.BankCommands.CreateBankAccountCommand;
using IOBBank.Application.Commands.BankCommands.CreateTransferCommand;
using IOBBank.Application.Commands.BankCommands.CreditCommand;
using IOBBank.Application.Commands.BankCommands.DebitAccountCommand;
using IOBBank.Application.Queries.BankQueries.BalanceAccountQuery;
using IOBBank.Application.Queries.BankQueries.BankAccountQuery;
using IOBBank.Application.Queries.BankQueries.TransferHistoryPaginatedQuery;
using IOBBank.Core.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOBBank.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class IOBBankController : BaseController
{
    private readonly IMediator _mediator;

    public IOBBankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("banck-account")]
    public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }

    [HttpPost("credit")]
    public async Task<IActionResult> CreditAccount([FromBody] CreateCreditInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }

    [HttpPost("debit")]
    public async Task<IActionResult> DebitAccount([FromBody] CreateDebitInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferAccount([FromBody] CreateTransferInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetBalanceAccount([FromQuery] BalanceAccountQueryInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }

    [HttpGet("history-transfer")]
    public async Task<IActionResult> PaginatedHistoryTransfer([FromQuery] TransferPaginatedQueryInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllBanck([FromQuery] BanckAccountQueryInput input)
    {
        var result = await _mediator.Send(input);
        return HandleResult(result);
    }
}