using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalizationEditor.BAL.Services.LocalizationStrings.Implementations
{
  internal class LocalizationStringService : ILocalizationStringService
  {
    private readonly ILocalizationStringRepository _repository;

    public LocalizationStringService(ILocalizationStringRepository repository)
    {
      _repository = repository;
    }

    public Task<ILocalizationRow> AddAsync(ILocalizationRow row)
    {
      return _repository.AddAsync(row);
    }

    public Task<ILocalizationRow> UpdateAsync(long id, ILocalizationRow row)
    {
      return _repository.UpdateAsync(row);
    }

    public Task<ILocalizationRow> GetByIdAsync(long id)
    {
      return _repository.GetByIdAsync(id);
    }

    public Task<IEnumerable<ILocalizationRow>> GetAllAsync()
    {
      return _repository.GetAllAsync();
    }

    public Task<long> DeleteAsync(ILocalizationRow row)
    {
      return _repository.DeleteAsync(row);
    }
  }
}