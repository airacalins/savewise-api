using API.Controllers.Accounts.InputModels;
using API.Controllers.Accounts.ViewModel;
using API.Controllers.InputModels;
using Application.Commands.Accounts.Interfaces;
using Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Accounts
{
  [ApiController]
  [Route("api/[controller]")]

  public class AccountsController : BaseController
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<AccountViewModel>>> GetAll()
    {
      var result = await _getAccountsCommand.ExecuteCommand();

      if (!result.IsSuccess) return BadRequest(result.Error);

      // var userId = GetCurrentLoggedInUserId();

      var data = result.Value
        // .Where(account => account.UserId == userId)
        .Select(account => new AccountViewModel(account))
        .ToList();
      return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountViewModel>> Get([FromRoute] Guid id)
    {
      var userId = GetCurrentLoggedInUserId();
      var result = await _getAccountCommand.ExecuteCommand(userId, id);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var data = new AccountViewModel(result.Value);
      return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountInputModel input)
    {
      var result = await _createAccountCommand.ExecuteCommand(input.ToCreateAccountDto(GetCurrentLoggedInUserId()));

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