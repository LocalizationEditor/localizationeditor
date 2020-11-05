using Auth.Application.Models;
using Auth.Domain.Models;
using LocalizationEditor.Base.Infrastructure;

namespace Auth.Application.Infrastructure
{
  public interface IUserAuthMapper : IMapper<IUserAuth, DbUserAuth>
  {
  }
}