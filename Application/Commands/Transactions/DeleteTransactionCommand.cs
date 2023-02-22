using Application.Commands.Transactions.Interfaces;
using Application.Core;
using Application.Repositories.AccountRepository;
using Application.Repositories.ActivityRepository;
using Application.Repositories.ActivityRepository.Dtos;
using Application.Repositories.TransactionRepository;
using Domain.Enums;

namespace Application.Commands.Transactions
{
  public class DeleteTransactionCommand : IDeleteTransactionCommand
  {
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IActivityRepository _activityRepository;
    public DeleteTransactionCommand(
      ITransactionRepository transactionRepository,
      IAccountRepository accountRepository,
      IActivityRepository activityRepository
    )
    {
      _transactionRepository = transactionRepository;
      _accountRepository = accountRepository;
      _activityRepository = activityRepository;
    }
    public async Task<Result<bool>> ExecuteCommand(Guid accountId, Guid id)
    {
      var account = await _accountRepository.GetById(accountId);
      if (account == null) return Result<bool>.Failure("Account not found");

      var transaction = await _transactionRepository.GetById(id);

      if (transaction == null) return Result<bool>.Failure("Transaction not found");

      var activity = new CreateActivityDto
      {
        AccountId = transaction.AccountId,
        TransactionId = id,
        ActivityType = ActivityType.Delete,
        DateCreated = DateTime.Now,
      };

      _activityRepository.Add(activity.ToActivityEntity());
      await _transactionRepository.Delete(id);
      await _transactionRepository.SaveChangesAsync();

      return Result<bool>.Success(true);
    }
  }
}