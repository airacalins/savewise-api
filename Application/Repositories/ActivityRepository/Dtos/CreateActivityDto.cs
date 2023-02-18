using Domain;

namespace Application.Repositories.ActivityRepository.Dtos
{
    public class CreateActivityDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime DateCreated { get; set; }

        public Activity ToActivityEntity()
        {
            return new Activity
            {
                Id = Id,
                AccountId = AccountId,
                TransactionId = TransactionId,
                DateCreated = DateCreated
            };
        }
    }
}