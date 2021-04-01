using LocalizationEditor.Admin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.Admin
{
  public interface IUserService
  {
    Task<IUser> Add(IUser user);
    Task<IEnumerable<IUser>> GetAll();
    Task<IUser> GetByEmail(string email);
    Task<Guid> Remove(Guid id);
    Task<IUser> Update(Guid id, IUser user);
  }
}