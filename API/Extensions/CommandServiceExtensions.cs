using Application.Commands.Accounts;
using Application.Commands.Accounts.Interfaces;
using Application.Commands.Activities;
using Application.Commands.Transactions;
using Application.Commands.Transactions.Interfaces;

namespace API.Extensions
{
  public static class CommandServiceExtensions
  {
    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {
      services.AddScoped<IGetAccountsCommand, GetAccountsCommand>();
      services.AddScoped<IGetAccountCommand, GetAccountCommand>();
      services.AddScoped<ICreateAccountCommand, CreateAccountCommand>();
      services.AddScoped<IUpdateAccountCommand, UpdateAccountCommand>();
      services.AddScoped<IDeleteAccountCommand, DeleteAccountCommand>();

      services.AddScoped<IGetTransactionsCommand, GetTransactionsCommand>();
      services.AddScoped<IGetTransactionCommand, GetTransactionCommand>();
      services.AddScoped<ICreateTransactionCommand, CreateTransactionCommand>();
      services.AddScoped<IUpdateTransactionCommand, UpdateTransactionCommand>();
      services.AddScoped<IDeleteTransactionCommand, DeleteTransactionCommand>();

      services.AddScoped<IGetActivitiesCommand, GetActivitiesCommand>();

      return services;
    }
  }
}