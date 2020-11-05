using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Infrastructure.Repository;
using Auth.Application.Models;
using MediatR;

namespace Auth.Application.Fetchers.Commands
{
  public class AddUserCommandHandler : IRequestHandler<AddUserCommand, IUserAuth>
  {
    private readonly IAuthRepository _repository;
    
    public AddUserCommandHandler(IAuthRepository repository)
    {
      _repository = repository;
    }
    
    public async Task<IUserAuth> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
      return await _repository.AddAsync(new UserAuth(default ,request.Email, request.Password));
    }
  }
}