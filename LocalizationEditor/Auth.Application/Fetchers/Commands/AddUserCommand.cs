using Auth.Application.Models;
using MediatR;

namespace Auth.Application.Fetchers.Commands
{
  public class AddUserCommand : IRequest<IUserAuth>
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}