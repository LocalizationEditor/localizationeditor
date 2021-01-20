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
using Microsoft.Data.SqlClient;

namespace LocalizationEditor.DAL.Repository
{
  public class LocalizationStringRepository : ILocalizationStringRepository
  {
    private readonly IDapperQueryHelper _queryHelper;

    public LocalizationStringRepository(IDapperQueryHelper queryHelper)
    {
      _queryHelper = queryHelper;
    }

    public Task<ILocalizationKey> AddAsync(ILocalizationKey localizationKey)
    {
      throw new NotImplementedException();
    }

    public Task<ILocalizationKey> UpdateAsync(ILocalizationKey localizationKey)
    {
      throw new System.NotImplementedException();
    }

    public Task<ILocalizationKey> GetByIdAsync(long id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<ILocalizationKey>> GetAllAsync(IDbConnection connection)
    {
      using (connection)
      {
        var map = new Dictionary<long, ILocalizationKey>();
        connection.Query<DbLocalizationString, DbLocalizationKey, DbLocalizationGroup, ILocalizationKey>(
          $@"{_queryHelper.GetSelectQuery("Localization_Strings", queryParam: "strs.*, locals.*, groups.*")}
                as strs inner join Localization as locals on strs.LocalizationId = locals.Id inner join Localization_Group as groups on locals.GroupId = groups.Id",
          (strs, keys, groups) =>
          {
            if (!map.TryGetValue(keys.Id, out var key))
            {
              var group = new LocalizationGroup(groups.Id, groups.Name);
              map.Add(keys.Id, new LocalizationKey(keys.Id, group, keys.Key, new List<ILocalizationPair>()));
            }

            (key ?? map[keys.Id]).AddLocalization(new LocalizationPair(strs.Locale, strs.Value));
            return key;
          });

        return map.Values;
      }
    }

    public Task<long> DeleteAsync(ILocalizationKey localizationKey)
    {
      throw new System.NotImplementedException();
    }
  }
}