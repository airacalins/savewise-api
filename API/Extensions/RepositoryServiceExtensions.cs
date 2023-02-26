using Application.Repositories.AccountRepositories;
using Application.Repositories.ActivityRepositories;
using Application.Repositories.TransactionRepositories;

namespace API.Extensions
{
  public static class RepositoryServiceExtensions
  {
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
      services.AddScoped<IAccountRepository, AccountRepository>();
      services.AddScoped<ITransactionRepository, TransactionRepository>();
      services.AddScoped<IActivityRepository, ActivityRepository>();

      return services;
    }
  }
}