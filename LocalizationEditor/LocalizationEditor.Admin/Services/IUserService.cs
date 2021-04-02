using LocalizationEditor.Admin.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.Admin.Services
{
  public interface IUserService
  {
    Task<IUser> Add(IUser user);
    Task<IEnumerable<IUser>> GetAll();
    Task<IUser> GetByEmail(string email);
    Task<Guid> Remove(Guid id);
    Task<IUser> Update(Guid id, IUser user);
    Task<IUser> Login(ILoginDto login);
    Task<IUser> GetById(Guid id);
  }
}