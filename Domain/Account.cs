using Domain.Enums;

namespace Domain
{
  public class Account
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public double Balance
    {
      get
      {
        var totalIncome = Transactions.Where(transaction => transaction.TransactionType == TransactionType.Income).Sum(transaction => transaction.Amount);
        var totalExpense = Transactions.Where(transaction => transaction.TransactionType == TransactionType.Expense).Sum(transaction => transaction.Amount);
        return totalIncome - totalExpense;
      }
    }
  }
}