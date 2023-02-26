using API.Controllers.Activities.ViewModels;
using Application.Commands.Activities;
using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Activities
{
  [ApiController]
  [Route("api/accounts/{accountId}/[controller]")]

  public class ActivitiesController : BaseController
  {
    private readonly IGetActivitiesCommand _getActivitiesCommand;
    public ActivitiesController(IGetActivitiesCommand getActivitiesCommand)
    {
      _getActivitiesCommand = getActivitiesCommand;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivityViewModel>>> GetAll(Guid accountId)
    {
      var result = await _getActivitiesCommand.ExecuteCommand(accountId);

      if (!result.IsSuccess) return BadRequest(result.Error);

      var data = result.Value.Select(activity => new ActivityViewModel(activity)).ToList();
      return Ok(data);
    }
  }
}