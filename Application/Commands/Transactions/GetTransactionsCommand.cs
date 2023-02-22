using Application.Commands.Transactions.Dtos;
using Application.Core;
using Application.Repositories.AccountRepository;
using Application.Repositories.TransactionRepository;

namespace Application.Commands.Transactions.Interfaces
{
  public class GetTransactionsCommand : IGetTransactionsCommand
  {
    private readonly ITransactionRepository _transactionRespository;
    private readonly IAccountRepository _accountRepository;

    public GetTransactionsCommand(
      ITransactionRepository transactionRespository,
      IAccountRepository accountRepository
    )
    {
      _transactionRespository = transactionRespository;
      _accountRepository = accountRepository;
    }

    public async Task<Result<List<TransactionDto>>> ExecuteCommand(Guid accountId)
    {
      var account = await _accountRepository.GetById(accountId);
      if (account == null) return Result<List<TransactionDto>>.Failure("Account not found");

      var transactions = await _transactionRespository.GetAll();
      var accountTransactions = transactions.Where(transaction => transaction.AccountId == accountId);

      var data = accountTransactions.Select(transaction => new TransactionDto(transaction)).ToList();

      return Result<List<TransactionDto>>.Success(data);
    }
  }
}