using System.Threading.Tasks;
using Auth.Application.Models;

namespace Auth.Application.Infrastructure.Repository
{
  public interface IAuthRepository
  {
    Task<IUserAuth> GetByEmailAndPasswordAsync(string email, string password);
    Task<IUserAuth> AddAsync(IUserAuth userAuth);
    Task UpdateAsync(IUserAuth userAuth);
  }
}