using API.Controllers.Activities.ViewModels;
using Application.Commands.Activities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Activities
{
  [ApiController]
  [Route("api/accounts/{accountId}/[controller]")]

  public class ActivitiesController : ControllerBase
  {
    private readonly IGetActivitiesCommand _getActivitiesCommand;
    public ActivitiesController(IGetActivitiesCommand getActivitiesCommand)
    {
      _getActivitiesCommand = getActivitiesCommand;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid accountId)
    {
      var result = await _getActivitiesCommand.ExecuteCommand(accountId);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var accountActivities = result.Value.Select(activity => new ActivityViewModel(activity)).ToList();

      return Ok(accountActivities);
    }
  }
}