using Domain;

namespace Application.Commands.Accounts.Dtos
{
  public class AccountDto
  {
    public AccountDto(Account account)
    {
      Id = account.Id;
      Title = account.Title;
      Balance = account.Balance;
      DateCreated = account.DateCreated;
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Balance { get; set; }
    public DateTime DateCreated { get; set; }
  }
}