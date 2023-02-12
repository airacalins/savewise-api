using Domain;

namespace Application.Commands.Accounts.Dtos
{
  public class CreateAccountDto
  {
    public string Title { get; set; } = String.Empty;

    public Account ToAccountEntity()
    {
      var account = new Account
      {
        Id = Guid.NewGuid(),
        Title = Title,
        DateCreated = DateTime.Now,
      };

      return account;
    }
  }
}