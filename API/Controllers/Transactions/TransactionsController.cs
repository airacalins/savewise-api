using API.Controllers.Transactions.InputModels;
using Application.Commands.Transactions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Transactions
{
  [ApiController]
  [Route("api/accounts/{accountId}/[controller]")]

  public class TransactionsController : ControllerBase
  {
    private readonly ICreateTransactionCommand _createTransactionCommand;
    public TransactionsController(ICreateTransactionCommand createTransactionCommand)
    {
      _createTransactionCommand = createTransactionCommand;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromRoute] Guid accountId, CreateTransactionInputModel item)
    {
      await _createTransactionCommand.ExecuteCommand(accountId, item.ToTransactionEntity());
      return Ok();
    }
  }
}