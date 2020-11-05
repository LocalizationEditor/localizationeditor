using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Infrastructure.Repository;
using Auth.Application.Models;
using MediatR;

namespace Auth.Application.Fetchers.Queries
{
  internal class GetByEmailAndPasswordQueryHandler : IRequestHandler<GetByEmailAndPasswordQuery, IUserAuth>
  {
    private readonly IAuthRepository _repository;

    public GetByEmailAndPasswordQueryHandler(IAuthRepository repository)
    {
      _repository = repository;
    }

    public async Task<IUserAuth> Handle(
      GetByEmailAndPasswordQuery request, CancellationToken cancellationToken)
    {
      return await _repository.GetByEmailAndPasswordAsync(request.Email, request.Password);
    }
  }
}