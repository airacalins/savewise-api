using Application.Commands.Transactions.Dtos;
using Domain.Enums;

namespace API.Controllers.Transactions.InputModels
{
  public class CreateTransactionInputModel
  {
    public TransactionType TransactionType { get; set; }
    public double Amount { get; set; }
    public DateTime DateCreated { get; set; }

    public CreateTransactionDto ToTransactionDto()
    {
      var transaction = new CreateTransactionDto
      {
        TransactionType = TransactionType,
        Amount = Amount,
        DateCreated = DateCreated,
      };

      return transaction;
    }
  }
}
