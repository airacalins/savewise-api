using Application.Commands.Transactions.Dtos;
using Application.Commands.Transactions.Interfaces;
using Application.Core;
using Application.Repositories.AccountRepository;
using Application.Repositories.ActivityRepository;
using Application.Repositories.ActivityRepository.Dtos;
using Application.Repositories.TransactionRepository;
using Domain.Enums;

namespace Application.Commands.Transactions
{
  public class UpdateTransactionCommand : IUpdateTransactionCommand
  {
    private readonly ITransactionRepository _transactionRepository;
    private readonly IActivityRepository _activityRepository;
    public UpdateTransactionCommand(
      ITransactionRepository transactionRepository,
      IActivityRepository activityRepository
    )
    {
      _transactionRepository = transactionRepository;
      _activityRepository = activityRepository;
    }
    public async Task<Result<bool>> ExecuteCommand(Guid id, UpdateTransactionDto input)
    {
      var transaction = await _transactionRepository.GetById(id);

      if (transaction == null) return Result<bool>.Failure("Transaction not found");
      if (input.Amount <= 0) return Result<bool>.Failure("Amount must be greater than zero");

      transaction.TransactionType = input.TransactionType;
      transaction.Amount = input.Amount;
      transaction.Date = input.DateCreated;

      var activity = new CreateActivityDto
      {
        AccountId = transaction.AccountId,
        TransactionId = transaction.Id,
        ActivityType = ActivityType.Update,
        DateCreated = DateTime.Now,
      };

      _activityRepository.Add(activity.ToActivityEntity());
      await _transactionRepository.SaveChangesAsync();

      return Result<bool>.Success(true);
    }
  }
}