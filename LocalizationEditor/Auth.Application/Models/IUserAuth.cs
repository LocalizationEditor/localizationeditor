using Domain.Entities.Base;

namespace Auth.Application.Models
{
  public interface IUserAuth : IIdEntity
  {
    string Email { get; }
    string Password { get; }
    void Update(IUserAuth userAuth);
  }
}