using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public abstract class BaseController : ControllerBase
  {
    protected string GetCurrentLoggedInUserId()
    {
      return User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
    }
  }
}