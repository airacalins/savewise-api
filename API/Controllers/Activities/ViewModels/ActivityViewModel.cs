using Application.Commands.Activities.Dtos;
using Domain.Enums;

namespace API.Controllers.Activities.ViewModels
{
  public class ActivityViewModel
  {
    public ActivityViewModel(ActivityDto item)
    {
      AccountId = item.AccountId;
      TransactionId = item.TransactionId;
      ActivityType = item.ActivityType;
      DateCreated = item.DateCreated;
    }
    public Guid AccountId { get; set; }
    public Guid TransactionId { get; set; }
    public ActivityType ActivityType { get; set; }
    public DateTime DateCreated { get; set; }
  }
}