using System.Collections.Generic;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Repositories
{
  public interface ILocalizationGroupRepository
  {
    Task<ILocalizationGroup> AddAsync(ILocalizationGroup localizationGroup);
    Task<ILocalizationGroup> UpdateAsync(ILocalizationGroup localizationString);
    Task<ILocalizationGroup> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationGroup>> GetAllAsync();
    Task<long> DeleteAsync(ILocalizationGroup localizationString);
  }
}