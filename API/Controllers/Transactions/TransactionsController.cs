using API.Controllers.Transactions.InputModels;
using Application.Commands.Transactions.Interfaces;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Transactions
{
  [ApiController]
  [Route("api/accounts/{accountId}/[controller]")]

  public class TransactionsController : BaseController
  {
    private readonly IGetTransactionsCommand _getTransactionsCommand;
    private readonly IGetTransactionCommand _getTransactionCommand;
    private readonly ICreateTransactionCommand _createTransactionCommand;
    private readonly IUpdateTransactionCommand _updateTransactionCommand;
    private readonly IDeleteTransactionCommand _deleteTransactionCommand;
    public TransactionsController(
      IGetTransactionsCommand getTransactionsCommand,
      IGetTransactionCommand getTransactionCommand,
      ICreateTransactionCommand createTransactionCommand,
      IUpdateTransactionCommand updateTransactionCommand,
      IDeleteTransactionCommand deleteTransactionCommand
    )
    {
      _getTransactionsCommand = getTransactionsCommand;
      _getTransactionCommand = getTransactionCommand;
      _createTransactionCommand = createTransactionCommand;
      _updateTransactionCommand = updateTransactionCommand;
      _deleteTransactionCommand = deleteTransactionCommand;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransactionViewModel>>> GetAll([FromRoute] Guid accountId)
    {
      var result = await _getTransactionsCommand.ExecuteCommand(accountId);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var data = result.Value.Select(transaction => new TransactionViewModel(transaction)).ToList();
      return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionViewModel>> Get([FromRoute] Guid accountId, [FromRoute] Guid id)
    {
      var result = await _getTransactionCommand.ExecuteCommand(accountId, id);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var data = new TransactionViewModel(result.Value);
      return Ok(data);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid accountId, CreateTransactionInputModel item)
    {
      var result = await _createTransactionCommand.ExecuteCommand(accountId, item.ToTransactionDto());

      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTransactionInputModel input)
    {
      var result = await _updateTransactionCommand.ExecuteCommand(id, input.ToUpdateTransactionDto());

      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid accountId, [FromRoute] Guid id)
    {
      var result = await _deleteTransactionCommand.ExecuteCommand(accountId, id);

      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }
  }
}