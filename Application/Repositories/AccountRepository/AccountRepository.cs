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
      return await _context.Accounts.ToListAsync();
    }

    public void Add(Account item)
    {
      _context.Accounts.Add(item);
    }
    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}