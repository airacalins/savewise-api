using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Contexts
{
    public interface IDataContext
    {
        DbSet<Account> Accounts {get;set;}
        DbSet<Transaction> Transactions {get;set;}
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

