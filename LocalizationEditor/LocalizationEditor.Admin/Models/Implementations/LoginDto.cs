namespace LocalizationEditor.Admin.Models.Implementations
{
  public class LoginDto : ILoginDto
  {
    public string Email { get; }
    public string Password { get; }

    public LoginDto(string email, string password)
    {
      Email = email;
      Password = password;
    }
  }
}