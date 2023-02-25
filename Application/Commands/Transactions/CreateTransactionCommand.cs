using Application.Commands.Activities.Dtos;
using Application.Commands.Transactions.Dtos;
using Application.Commands.Transactions.Interfaces;
using Application.Core;
using Application.Repositories.ActivityRepositories;
using Application.Repositories.TransactionRepositories;
using Domain;
using Domain.Enums;

namespace Application.Commands.Transactions
{
    public class CreateTransactionCommand : ICreateTransactionCommand
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IActivityRepository _activityRepository;

        public CreateTransactionCommand(ITransactionRepository transactionRepository, IActivityRepository activityRepository)
        {
            _transactionRepository = transactionRepository;
            _activityRepository = activityRepository;
        }

        public async Task<Result<bool>> ExecuteCommand(Guid accountId, CreateTransactionDto input)
        {
            if (input.Amount <= 0) return Result<bool>.Failure("Amount must be greater than zero");

            var transaction = new CreateTransactionDto
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                Amount = input.Amount,
                TransactionType = input.TransactionType,
                DateCreated = DateTime.Now,
            };

            var activity = new CreateActivityDto
            {
                AccountId = accountId,
                TransactionId = transaction.Id,
                ActivityType = ActivityType.Create,
                DateCreated = DateTime.Now,
            };

            _transactionRepository.Add(transaction.ToTransactionEntity());
            _activityRepository.Add(activity.ToActivityEntity());
            await _transactionRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}