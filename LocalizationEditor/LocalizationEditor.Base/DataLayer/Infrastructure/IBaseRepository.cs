using System.Threading.Tasks;

namespace Persistence.Base
{
  internal interface IBaseRepository<TDomain>
  {
    Task<TDomain> AddAsync(TDomain domain);
    Task<TDomain> GetByIdAsync(long id);
    Task UpdateAsync(TDomain domain);
    Task DeleteAsync(TDomain domain);
  }
}