namespace Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public DateTime DateCreated { get; set; }
    }
}