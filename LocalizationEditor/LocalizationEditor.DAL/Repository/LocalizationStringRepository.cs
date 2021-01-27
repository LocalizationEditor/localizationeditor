using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.Base.DapperHelper;
using LocalizationEditor.DAL.Models;

namespace LocalizationEditor.DAL.Repository
{
  public class LocalizationStringRepository : ILocalizationStringRepository
  {
    public Task<ILocalizationKey> AddAsync(ILocalizationKey localizationKey, IDbConnection connection)
    {
      throw new NotImplementedException();
    }

    public Task<ILocalizationKey> UpdateAsync(ILocalizationKey localizationKey,  IDbConnection connection)
    {
      throw new System.NotImplementedException();
    }

    public Task<ILocalizationKey> GetByIdAsync(long id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<ILocalizationKey>> GetAllAsync(IDbConnection connection)
    {
      throw new NotImplementedException();
    }

    public Task<long> DeleteAsync(ILocalizationKey localizationKey, IDbConnection connection)
    {
      throw new System.NotImplementedException();
    }
  }
}