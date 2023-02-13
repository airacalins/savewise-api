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
        private readonly ICreateTransactionCommand _createTransactionCommand;
        public TransactionsController(
          IGetTransactionsCommand getTransactionsCommand,
          ICreateTransactionCommand createTransactionCommand
        )
        {
            _getTransactionsCommand = getTransactionsCommand;
            _createTransactionCommand = createTransactionCommand;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromRoute] Guid accountId, CreateTransactionInputModel item)
        {
            await _createTransactionCommand.ExecuteCommand(accountId, item.ToTransactionDto());
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionViewModel>>> GetAll()
        {
            var transactions = await _getTransactionsCommand.ExecuteCommand();
            return transactions.Select(transaction => new TransactionViewModel(transaction)).ToList();
        }
    }
}