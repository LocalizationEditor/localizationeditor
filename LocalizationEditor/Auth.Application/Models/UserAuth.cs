namespace Auth.Application.Models
{
  internal class UserAuth : IUserAuth
  {
    public long Id { get; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public UserAuth(long id, string email, string password)
    {
      Id = id;
      Email = email;
      Password = password;
    }

    public void Update(IUserAuth userAuth)
    {
      Email = userAuth.Email;
      Password = userAuth.Password;
    }
  }
}