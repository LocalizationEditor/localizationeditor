using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Services.LocalizationStrings
{
  internal interface ILocalizationStringService
  {
    Task<ILocalizationString> AddAsync(ILocalizationString @string);
    Task<ILocalizationString> UpdateAsync(long id, ILocalizationString @string);
    Task<ILocalizationString> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationString>> GetAllAsync();
    Task<long> DeleteAsync(ILocalizationString @string);
  }
}