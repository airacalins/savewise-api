using Application.Commands.Accounts.Dtos;

namespace API.Controllers.Accounts.ViewModel
{
  public class AccountViewModel
  {
    public AccountViewModel(AccountDto accountDto)
    {
      Id = accountDto.Id;
      Title = accountDto.Title;
      Balance = accountDto.Balance;
      DateCreated = accountDto.DateCreated;
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Balance { get; set; }
    public DateTime DateCreated { get; set; }
  }
}