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
    public async Task<ActionResult<List<TransactionViewModel>>> GetAll()
    {
      var transactions = await _getTransactionsCommand.ExecuteCommand();
      return transactions.Select(transaction => new TransactionViewModel(transaction)).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionViewModel>> Get([FromRoute] Guid accountId, [FromRoute] Guid id)
    {
      var transaction = await _getTransactionCommand.ExecuteCommand(accountId, id);
      return new TransactionViewModel(transaction);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromRoute] Guid accountId, CreateTransactionInputModel item)
    {
      await _createTransactionCommand.ExecuteCommand(accountId, item.ToTransactionDto());
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] Guid accountId, [FromRoute] Guid id, [FromBody] UpdateTransactionInputModel input)
    {
      await _updateTransactionCommand.ExecuteCommand(accountId, id, input.ToUpdateTransactionDto());
      return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
      await _deleteTransactionCommand.ExecuteCommand(id);
      return Ok();
    }
  }
}