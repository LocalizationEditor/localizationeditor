using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Services.LocalizationStrings
{
  internal interface ILocalizationStringService
  {
    Task<ILocalizationKey> AddAsync(ILocalizationKey key);
    Task<ILocalizationKey> UpdateAsync(long id, ILocalizationKey key);
    Task<ILocalizationKey> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationKey>> GetAllAsync();
    Task<long> DeleteAsync(ILocalizationKey key);
  }
}