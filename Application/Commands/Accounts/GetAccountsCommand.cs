using Application.Commands.Accounts.Dtos;
using Application.Commands.Accounts.Interfaces;
using Application.Repositories.AccountRepository;

namespace Application.Commands.Accounts
{
  public class GetAccountsCommand : IGetAccountsCommand
  {
    private readonly IAccountRepository _accountRepository;

    public GetAccountsCommand(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }
    public async Task<List<AccountDto>> ExecuteCommand()
    {
      var accounts = await _accountRepository.GetAll();
      return accounts.Where(account => !account.IsArchived).Select(account => new AccountDto(account)).ToList();
    }
  }
}