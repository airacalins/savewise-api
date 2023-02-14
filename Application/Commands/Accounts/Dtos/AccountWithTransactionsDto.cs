using Application.Commands.Transactions.Dtos;
using Domain;

namespace Application.Commands.Accounts.Dtos
{
    public class AccountWithTransactionsDto
    {
        public AccountWithTransactionsDto(Account input)
        {
            Id = input.Id;
            Title = input.Title;
            Transactions = input.Transactions.Select(transaction => new TransactionDto(transaction)).ToList();
            Balance = input.Balance;
            DateCreated = input.DateCreated;
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<TransactionDto> Transactions { get; set; }
        public double Balance { get; set; }
        public DateTime DateCreated { get; set; }
    }
}