using System.Threading.Tasks;
using ConnectionString.Models;

namespace ConnectionString.Infrastructure
{
  public interface IConnectionProvider
  {
    Task<IConnection> GetById(long id);
    Task<IConnection> GetAll();
    Task<IConnection> Add(IConnection entity);
    Task<IConnection> Update(IConnection entity);
    Task Delete(IConnection entity);
  }
}