using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Repositories
{
  public interface ILocalizationGroupRepository
  {
    Task<ILocalizationGroup> AddAsync(ILocalizationGroup model);
    Task<IEnumerable<ILocalizationGroup>> GetAllAsync();
    Task<ILocalizationGroup> SearchByGroupKeyAsync(string groupKey);
    Task<ILocalizationGroup> UpdateAsync(ILocalizationGroup model);
  }
}