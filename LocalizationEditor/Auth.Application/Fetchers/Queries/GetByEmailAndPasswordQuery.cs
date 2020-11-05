using Auth.Application.Models;
using MediatR;

namespace Auth.Application.Fetchers.Queries
{
  public class GetByEmailAndPasswordQuery : IRequest<IUserAuth>
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}