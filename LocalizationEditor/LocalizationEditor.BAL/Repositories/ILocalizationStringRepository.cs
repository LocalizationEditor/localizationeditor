using System.Collections.Generic;
using System.Data;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Repositories
{
  public interface ILocalizationStringRepository
  {
    Task<ILocalizationKey> AddAsync(ILocalizationKey localizationKey, IDbConnection connection);
    Task<ILocalizationKey> UpdateAsync(ILocalizationKey localizationKey, IDbConnection connection);
    Task<ILocalizationKey> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationKey>> GetAllAsync(IDbConnection connection);
    Task<long> DeleteAsync(ILocalizationKey localizationKey, IDbConnection connection);
  }
}