namespace API.Controllers.Users.ViewModels
{
  public class UserViewModel
  {
    public UserViewModel(string username, string token)
    {
      Username = username;
      Token = token;
    }
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
  }
}