using Application.Commands.Transactions.Dtos;
using Application.Commands.Transactions.Interfaces;
using Application.Repositories.ActivityRepository;
using Application.Repositories.ActivityRepository.Dtos;
using Application.Repositories.TransactionRepository;
using Domain;

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

        public async Task ExecuteCommand(Guid accountId, CreateTransactionDto item)
        {

            var transaction = new CreateTransactionDto
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                Amount = item.Amount,
                TransactionType = item.TransactionType,
                DateCreated = DateTime.Now,
            };

            var activity = new CreateActivityDto
            {
                AccountId = accountId,
                TransactionId = transaction.Id,
                DateCreated = DateTime.Now,
            };

            _transactionRepository.Add(transaction.ToTransactionEntity());
            _activityRepository.Add(activity.ToActivityEntity());
            await _transactionRepository.SaveChangesAsync();
        }
    }
}