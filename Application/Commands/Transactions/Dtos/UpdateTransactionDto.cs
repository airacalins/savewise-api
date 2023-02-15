using Domain;
using Domain.Enums;

namespace Application.Commands.Transactions.Dtos
{
  public class UpdateTransactionDto
  {
    public TransactionType TransactionType { get; set; }
    public double Amount { get; set; }
    public DateTime DateCreated { get; set; }

    public Transaction ToTransactionEntity()
    {
      var transaction = new Transaction
      {
        TransactionType = TransactionType,
        Amount = Amount,
        DateCreated = DateCreated,
      };

      return transaction;
    }
  }
}