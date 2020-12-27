using System.Collections.Generic;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Repositories
{
  public interface ILocalizationStringRepository
  {
    Task<ILocalizationRow> AddAsync(ILocalizationRow row);
    Task<ILocalizationRow> UpdateAsync(ILocalizationRow row);
    Task<ILocalizationRow> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationRow>> GetAllAsync();
    Task<long> DeleteAsync(ILocalizationRow row);
  }
}