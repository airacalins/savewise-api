using Application.Commands.Transactions.Dtos;
using Domain.Enums;

namespace API.Controllers.Transactions
{
    public class TransactionViewModel
    {
        public TransactionViewModel(TransactionDto item)
        {
            Id = item.Id;
            TransactionType = item.TransactionType;
            Amount = item.Amount;
            DateCreated = item.DateCreated;
        }

        public Guid Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}