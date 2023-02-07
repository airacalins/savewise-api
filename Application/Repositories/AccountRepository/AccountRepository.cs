using Application.Contexts;
using Domain;

namespace Application.Repositories.AccountRepository
{
  public class AccountRepository : IAccountRepository
  {
    private readonly IDataContext _context;
    public AccountRepository(IDataContext context) 
    {
      _context = context;
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