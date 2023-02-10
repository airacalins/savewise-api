using Application.Commands.Accounts.Dtos;
using Domain;

namespace Application.Commands.Accounts.Interfaces
{
  public interface IGetAccountsCommand
  {
    Task<List<AccountDto>> ExecuteCommand();
  }
}