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

    public Task<ILocalizationString> AddAsync(ILocalizationString @string)
    {
      return _repository.AddAsync(@string);
    }

    public Task<ILocalizationString> UpdateAsync(long id, ILocalizationString @string)
    {
      return _repository.UpdateAsync(@string);
    }

    public Task<ILocalizationString> GetByIdAsync(long id)
    {
      return _repository.GetByIdAsync(id);
    }

    public Task<IEnumerable<ILocalizationString>> GetAllAsync()
    {
      return _repository.GetAllAsync();
    }

    public Task<long> DeleteAsync(ILocalizationString @string)
    {
      return _repository.DeleteAsync(@string);
    }
  }
}