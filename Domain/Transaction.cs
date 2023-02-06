using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = default!;
        public double Amount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}