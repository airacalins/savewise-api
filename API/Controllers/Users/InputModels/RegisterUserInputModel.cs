using System.ComponentModel.DataAnnotations;
using Domain;

namespace API.Controllers.Users.InputModels
{
  public class RegisterUserInputModel
  {
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password must be complex")]
    public string Password { get; set; } = string.Empty;

    public User ToUserEntity()
    {
      return new User
      {
        FirstName = FirstName,
        LastName = LastName,
        UserName = Username,
        Email = Email,
      };
    }
  }
}