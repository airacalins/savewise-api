using Application.Commands.Accounts;
using Application.Commands.Accounts.Interfaces;
using Application.Commands.Transactions;
using Application.Commands.Transactions.Interfaces;
using Application.Contexts;
using Application.Repositories.AccountRepository;
using Application.Repositories.TransactionRepository;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Connection
builder.Services.AddDbContext<DataContext>(opt =>
    {
        opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

// Dependency Injection
builder.Services.AddScoped<IDataContext, DataContext>();

// Dependency Injection (CQRS)
builder.Services.AddScoped<IGetAccountsCommand, GetAccountsCommand>();
builder.Services.AddScoped<IGetAccountCommand, GetAccountCommand>();
builder.Services.AddScoped<ICreateAccountCommand, CreateAccountCommand>();
builder.Services.AddScoped<IUpdateAccountCommand, UpdateAccountCommand>();
builder.Services.AddScoped<IDeleteAccountCommand, DeleteAccountCommand>();
builder.Services.AddScoped<IGetTransactionsCommand, GetTransactionsCommand>();
builder.Services.AddScoped<IGetTransactionCommand, GetTransactionCommand>();
builder.Services.AddScoped<ICreateTransactionCommand, CreateTransactionCommand>();

// Dependency Injection (Repository)
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
