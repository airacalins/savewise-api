using API.Controllers.Accounts.ViewModel;
using API.Controllers.InputModels;
using Application.Commands.Accounts;
using Application.Commands.Accounts.Dtos;
using Application.Commands.Accounts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Accounts
{
  [ApiController]
  [Route("api/[controller]")]

  public class AccountsController : ControllerBase
  {
    private readonly IGetAccountsCommand _getAccountsCommand;
    private readonly ICreateAccountCommand _createAccountCommand;
    public AccountsController(
      IGetAccountsCommand getAccountsCommand,
      ICreateAccountCommand createAccountCommand
    )
    {
      _getAccountsCommand = getAccountsCommand;
      _createAccountCommand = createAccountCommand;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountViewModel>>> GetAccounts()
    {
      var accounts = await _getAccountsCommand.ExecuteCommand();
      return accounts.Select(account => new AccountViewModel(account)).ToList();
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount(CreateAccountInputModel input)
    {
      await _createAccountCommand.ExecuteCommand(input.ToDto());
      return Ok();
    }
  }
}