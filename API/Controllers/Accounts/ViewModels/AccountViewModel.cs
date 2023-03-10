using Application.Commands.Accounts.Dtos;

namespace API.Controllers.Accounts.ViewModel
{
    public class AccountViewModel
    {
        public AccountViewModel(AccountDto item)
        {
            Id = item.Id;
            Title = item.Title;
            Balance = item.Balance;
            DateCreated = item.DateCreated;
        }
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Balance { get; set; }
        public DateTime DateCreated { get; set; }
    }
}