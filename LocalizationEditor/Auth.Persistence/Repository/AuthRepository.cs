using System.Threading.Tasks;
using Auth.Application.Infrastructure;
using Auth.Application.Infrastructure.Repository;
using Auth.Application.Models;
using Auth.Domain.Models;
using Auth.Persistence.Context;
using LocalizationEditor.Base.DataLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Auth.Persistence.Repository
{
  internal class AuthRepository
    : BaseRepository<IUserAuth, DbUserAuth>, IAuthRepository
  {
    public AuthRepository(
      AuthContext context,
      IUserAuthMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IUserAuth> GetByEmailAndPasswordAsync(string email, string password)
    {
      var model = await Context.Set<DbUserAuth>()
        .FirstOrDefaultAsync(i => i.Email == email && i.Password == password);
      
      return model == null ? null : Mapper.GetModel(model);
    }
  }
}