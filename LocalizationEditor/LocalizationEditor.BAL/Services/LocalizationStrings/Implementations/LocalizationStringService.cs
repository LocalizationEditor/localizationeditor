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

    public Task<ILocalizationKey> AddAsync(ILocalizationKey key)
    {
      return _repository.AddAsync(key);
    }

    public Task<ILocalizationKey> UpdateAsync(long id, ILocalizationKey key)
    {
      return _repository.UpdateAsync(key);
    }

    public Task<ILocalizationKey> GetByIdAsync(long id)
    {
      return _repository.GetByIdAsync(id);
    }

    public Task<IEnumerable<ILocalizationKey>> GetAllAsync()
    {
      return _repository.GetAllAsync();
    }

    public Task<long> DeleteAsync(ILocalizationKey key)
    {
      return _repository.DeleteAsync(key);
    }
  }
}