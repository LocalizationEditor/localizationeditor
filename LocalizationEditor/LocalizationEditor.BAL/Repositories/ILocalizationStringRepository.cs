using System.Collections.Generic;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Repositories
{
  public interface ILocalizationStringRepository
  {
    Task<ILocalizationString> AddAsync(ILocalizationString localizationString);
    Task<ILocalizationString> UpdateAsync(ILocalizationString localizationString);
    Task<ILocalizationString> GetByIdAsync(long id);
    Task<IEnumerable<ILocalizationString>> GetAllAsync();
    Task<long> DeleteAsync(ILocalizationString localizationString);
    void SetConnectionString(string connectionString);
  }
}