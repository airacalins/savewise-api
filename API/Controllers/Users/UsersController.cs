using System.Security.Claims;
using API.Controllers.Users.InputModels;
using API.Controllers.Users.ViewModels;
using API.Services;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Users
{
  [ApiController]
  [Route("api/[controller]")]

  public class UsersController : BaseController
  {
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    public UsersController(
        UserManager<User> userManager,
        TokenService tokenService
    )
    {
      _userManager = userManager;
      _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserViewModel>> Login(LoginUserInputModel input)
    {
      var user = await _userManager.FindByEmailAsync(input.Email);
      var token = _tokenService.CreateToken(user);

      if (user == null) return Unauthorized();

      var result = await _userManager.CheckPasswordAsync(user, input.Password);

      if (!result) return Unauthorized();

      return new UserViewModel(user.UserName, token);

    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserViewModel>> Register(RegisterUserInputModel input)
    {
      if (await _userManager.Users.AnyAsync(user => user.UserName == input.Username))
      {
        return BadRequest("Username is already taken");
      }

      if (await _userManager.Users.AnyAsync(user => user.Email == input.Email))
      {
        return BadRequest("Email is already taken");
      }

      var user = input.ToUserEntity();
      var token = _tokenService.CreateToken(user);

      var result = await _userManager.CreateAsync(user, input.Password);

      if (!result.Succeeded) return BadRequest(result.Errors);

      return new UserViewModel(input.Username, token);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserViewModel>> GetCurrentUser()
    {
      var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
      var token = _tokenService.CreateToken(user);

      return new UserViewModel(user.UserName, token);
    }
  }
}