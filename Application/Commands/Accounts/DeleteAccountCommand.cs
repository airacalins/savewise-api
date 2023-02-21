using Application.Commands.Accounts.Interfaces;
using Application.Core;
using Application.Repositories.AccountRepository;

namespace Application.Commands.Accounts
{
  public class DeleteAccountCommand : IDeleteAccountCommand
  {
    private readonly IAccountRepository _accountRepository;

    public DeleteAccountCommand(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }
    public async Task<Result<bool>> ExecuteCommand(Guid id)
    {
      var account = await _accountRepository.GetById(id);

      if (account == null) return Result<bool>.Failure("Account Not Found");

      await _accountRepository.Delete(id);
      await _accountRepository.SaveChangesAsync();

      return Result<bool>.Success(true);
    }
  }
}