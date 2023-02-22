using API.Controllers.Transactions.InputModels;
using Application.Commands.Transactions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Transactions
{
  [ApiController]
  [Route("api/accounts/{accountId}/[controller]")]

  public class TransactionsController : ControllerBase
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
    public async Task<IActionResult> GetAll([FromRoute] Guid accountId)
    {
      var result = await _getTransactionsCommand.ExecuteCommand(accountId);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var transactions = result.Value.Select(transaction => new TransactionViewModel(transaction)).ToList();

      return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid accountId, [FromRoute] Guid id)
    {
      var result = await _getTransactionCommand.ExecuteCommand(accountId, id);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var transaction = new TransactionViewModel(result.Value);

      return Ok(transaction);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid accountId, CreateTransactionInputModel item)
    {
      var result = await _createTransactionCommand.ExecuteCommand(accountId, item.ToTransactionDto());

      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTransactionInputModel input)
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