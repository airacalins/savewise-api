using Application.Commands.Transactions.Dtos;
using Domain;

namespace Application.Commands.Accounts.Dtos
{
  public class AccountDto
  {
    public AccountDto(Account account)
    {
      Id = account.Id;
      UserId = account.UserId;
      Title = account.Title;
      Balance = account.Balance;
      Transactions = account.Transactions.Select(transaction => new TransactionDto(transaction)).ToList();
      DateCreated = account.DateCreated;
    }
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public double Balance { get; set; }
    public List<TransactionDto> Transactions { get; set; }
    public DateTime DateCreated { get; set; }
  }
}