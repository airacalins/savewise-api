using API.Controllers.Accounts.InputModels;
using API.Controllers.Accounts.ViewModel;
using API.Controllers.InputModels;
using Application.Commands.Accounts.Interfaces;
using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Accounts
{
  [ApiController]
  [Route("api/[controller]")]

  public class AccountsController : ControllerBase
  {
    private readonly IGetAccountsCommand _getAccountsCommand;
    private readonly IGetAccountCommand _getAccountCommand;
    private readonly ICreateAccountCommand _createAccountCommand;
    private readonly IUpdateAccountCommand _updateAccountCommand;
    private readonly IDeleteAccountCommand _deleteAccountCommand;

    public AccountsController(
      IGetAccountsCommand getAccountsCommand,
      IGetAccountCommand getAccountCommand,
      ICreateAccountCommand createAccountCommand,
      IUpdateAccountCommand updateAccountCommand,
      IDeleteAccountCommand deleteAccountCommand
    )
    {
      _getAccountsCommand = getAccountsCommand;
      _getAccountCommand = getAccountCommand;
      _createAccountCommand = createAccountCommand;
      _updateAccountCommand = updateAccountCommand;
      _deleteAccountCommand = deleteAccountCommand;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var accounts = await _getAccountsCommand.ExecuteCommand();
      var result = accounts.Value
        .Select(account => new AccountViewModel(account))
        .ToList();

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
      var account = await _getAccountCommand.ExecuteCommand(id);

      if (!account.IsSuccess) return BadRequest(account.Error);

      var result = new AccountViewModel(account.Value);
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountInputModel input)
    {
      var result = await _createAccountCommand.ExecuteCommand(input.ToCreateAccountDto());
      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAccountInputModel input)
    {
      var result = await _updateAccountCommand.ExecuteCommand(id, input.ToUpdateAccountDto());
      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      var result = await _deleteAccountCommand.ExecuteCommand(id);
      if (!result.IsSuccess) return BadRequest(result.Error);

      return Ok(result.Value);
    }

  }
}