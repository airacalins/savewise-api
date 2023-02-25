using Application.Commands.Activities.Dtos;
using Application.Core;
using Application.Repositories.AccountRepositories;
using Application.Repositories.ActivityRepositories;

namespace Application.Commands.Activities
{
    public class GetActivitiesCommand : IGetActivitiesCommand
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IAccountRepository _accountRepository;
        public GetActivitiesCommand(
            IActivityRepository activityRepository,
            IAccountRepository accountRepository
        )
        {
            _activityRepository = activityRepository;
            _accountRepository = accountRepository;
        }
        public async Task<Result<List<ActivityDto>>> ExecuteCommand(Guid accountId)
        {
            var account = await _accountRepository.GetById(accountId);
            if (account == null) return Result<List<ActivityDto>>.Failure("Account not found");

            var activities = await _activityRepository.GetAll();
            var accountActivities = activities.Where(activity => activity.AccountId == accountId);
            var data = accountActivities.Select(activity => new ActivityDto(activity)).ToList();

            return Result<List<ActivityDto>>.Success(data);
        }
    }
}