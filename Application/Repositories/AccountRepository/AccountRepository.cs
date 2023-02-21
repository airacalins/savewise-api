using Application.Contexts;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.AccountRepository
{
  public class AccountRepository : IAccountRepository
  {
    private readonly IDataContext _context;
    public AccountRepository(IDataContext context)
    {
      _context = context;
    }
    public async Task<List<Account>> GetAll()
    {
      return await _context.Accounts.Include(transaction => transaction.Transactions).ToListAsync();
    }

    public async Task<Account> GetById(Guid id)
    {
      return await _context.Accounts.Include(transaction => transaction.Transactions).FirstOrDefaultAsync(account => account.Id == id && !account.IsArchived);
    }

    public void Add(Account item)
    {
      _context.Accounts.Add(item);
    }

    public async Task Update(Account item)
    {
      var account = await GetById(item.Id);
      account = item;
    }

    public async Task Delete(Guid id)
    {
      var account = await GetById(id);
      account.IsArchived = true;
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}