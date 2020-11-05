using Auth.Application.Models;
using Auth.Domain.Models;

namespace Auth.Application.Infrastructure
{
  internal class UserAuthMapper : IUserAuthMapper
  {
    public DbUserAuth GetModel(IUserAuth second)
    {
      return new DbUserAuth
      {
        Id = second.Id,
        Email = second.Email,
        Password = second.Password
      };
    }

    public IUserAuth GetModel(DbUserAuth first)
    {
      return new UserAuth(first.Id, first.Email, first.Password);
    }
  }
}