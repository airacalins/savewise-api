using API.Controllers.Accounts.InputModels;
using API.Controllers.Accounts.ViewModel;
using API.Controllers.InputModels;
using Application.Commands.Accounts.Interfaces;
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

        public AccountsController(
          IGetAccountsCommand getAccountsCommand,
          IGetAccountCommand getAccountCommand,
          ICreateAccountCommand createAccountCommand,
          IUpdateAccountCommand updateAccountCommand
        )
        {
            _getAccountsCommand = getAccountsCommand;
            _getAccountCommand = getAccountCommand;
            _createAccountCommand = createAccountCommand;
            _updateAccountCommand = updateAccountCommand;
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountViewModel>>> GetAccounts()
        {
            var accounts = await _getAccountsCommand.ExecuteCommand();
            return accounts.Select(account => new AccountViewModel(account)).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountViewModel>> GetAccount([FromRoute] Guid id)
        {
            var account = await _getAccountCommand.ExecuteCommand(id);
            return new AccountViewModel(account);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountInputModel input)
        {
            await _createAccountCommand.ExecuteCommand(input.ToDto());
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] UpdateAccountInputModel input)
        {
            await _updateAccountCommand.ExecuteCommand(id, input.ToDto());
            return Ok();
        }

    }
}