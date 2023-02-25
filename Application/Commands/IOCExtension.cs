using Application.Commands.Accounts;
using Application.Commands.Accounts.Interfaces;
using Application.Commands.Activities;
using Application.Commands.Transactions;
using Application.Commands.Transactions.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commands
{
    public static class IOCExtension
    {
        public static void AddCommands(this IServiceCollection service)
        {
            // Dependency Injection - Account
            service.AddScoped<IGetAccountsCommand, GetAccountsCommand>();
            service.AddScoped<IGetAccountCommand, GetAccountCommand>();
            service.AddScoped<ICreateAccountCommand, CreateAccountCommand>();
            service.AddScoped<IUpdateAccountCommand, UpdateAccountCommand>();
            service.AddScoped<IDeleteAccountCommand, DeleteAccountCommand>();

            // Dependency Injection - Transaction
            service.AddScoped<IGetTransactionsCommand, GetTransactionsCommand>();
            service.AddScoped<IGetTransactionCommand, GetTransactionCommand>();
            service.AddScoped<ICreateTransactionCommand, CreateTransactionCommand>();
            service.AddScoped<IUpdateTransactionCommand, UpdateTransactionCommand>();
            service.AddScoped<IDeleteTransactionCommand, DeleteTransactionCommand>();

            // Dependency Injection - Activity
            service.AddScoped<IGetActivitiesCommand, GetActivitiesCommand>();
        }
    }
}