using Application.Commands.Accounts.Dtos;
using Application.Commands.Accounts.Interfaces;
using Application.Repositories.AccountRepository;
using Domain;

namespace Application.Commands.Accounts
{
    public class CreateAccountCommand : ICreateAccountCommand
    {
        private readonly IAccountRepository _accountRepository;
        public CreateAccountCommand(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task ExecuteCommand(CreateAccountDto input)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Title = input.Title,
                Balance = 0,
                DateCreated = DateTime.Now,
            };

            _accountRepository.Add(account);
            await _accountRepository.SaveChangesAsync();
        }
    }
}