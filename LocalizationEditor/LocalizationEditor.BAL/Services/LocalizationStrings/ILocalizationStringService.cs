using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Services.LocalizationStrings
{
  internal interface ILocalizationStringService
  {
    Task<ILocalizationRow> AddAsync(ILocalizationRow row);
    Task<ILocalizationRow> UpdateAsync(long id, ILocalizationRow row);
    Task<ILocalizationRow> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationRow>> GetAllAsync();
    Task<long> DeleteAsync(ILocalizationRow row);
  }
}