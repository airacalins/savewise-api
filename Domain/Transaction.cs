using Domain.Enums;

namespace Domain
{
  public class Transaction
  {
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = default!;
    public TransactionType TransactionType { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
  }
}