using Application.Commands.Transactions.Dtos;
using Application.Repositories.TransactionRepository;

namespace Application.Commands.Transactions.Interfaces
{
  public class GetTransactionsCommand : IGetTransactionsCommand
  {
    private readonly ITransactionRepository _transactionRespository;

    public GetTransactionsCommand(ITransactionRepository transactionRespository)
    {
      _transactionRespository = transactionRespository;
    }
    public async Task<List<TransactionDto>> ExecuteCommand(Guid accountId)
    {
      var transactions = await _transactionRespository.GetAll();
      var accountTransactions = transactions.Where(transaction => transaction.AccountId == accountId);
      return accountTransactions.Where(account => !account.IsArchived).Select(transaction => new TransactionDto(transaction)).ToList();
    }
  }
}