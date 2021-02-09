using Dapper;
using LocalizationEditor.BAL.Configurations;
using LocalizationEditor.BAL.Models;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalizationStringModel = LocalizationEditor.BAL.Models.LocalizationString.Implementations.LocalizationString;

namespace LocalizationEditor.DAL.Repository.LocalizationString
{

  public static class SQLParameterHelper
  {
    public const string KeyParameter = "@Key";
    public const string GroupParameter = "@GroupId";
    public const string IdParameter = "@Id";
    public const string NameParameter = "@Name";
  }

  internal class LocalizationStringRepository : SqlServerDapperDao<ILocalizationString>, ILocalizationStringRepository
  {
    private const string query = @"select groups.[Id] as GroupId,
                        groups.[Name] as GroupName,
                        strings.[Id] as StringId,
                        strings.[StringKey] as [Key],
                        {0}
                       from {1} strings
                          join {2} groups on groups.[Id] = strings.LocalizationTypeId";

    private readonly ITablesConfigurationOptions _tableNamingOptions;


    public LocalizationStringRepository(ITablesConfigurationOptions tableNamingOptions)
    {

      _tableNamingOptions = tableNamingOptions;
    }

    public ILocalizationStringRepository SetConnectionString(string connectionString)
    {
      ConnectionString = connectionString;
      return this;
    }

    public async override Task<ILocalizationString> AddAsync(ILocalizationString model)
    {
      var columns = string.Join(",", model.Localizations.Select(i => $"[{i.Locale}]"));
      var columnParametres = string.Join(",", model.Localizations.Select(i => $"@{i.Locale}"));
      var parameters = GetParameters(model);

      var sql = $@"insert {_tableNamingOptions.LocalizationStringsTableName} ([StringKey], [LocalizationTypeId], {columns})
                  values ({SQLParameterHelper.KeyParameter},{SQLParameterHelper.GroupParameter},{columnParametres})
                  select cast(scope_identity() as int) as Id";
      var newId = await GetConnection().QueryFirstAsync(sql, parameters);
      model = await GetByIdAsync(newId.Id);
      return model;
    }



    public async override Task<ILocalizationString> UpdateAsync(ILocalizationString model)
    {
      var sql = $@"update {_tableNamingOptions.LocalizationStringsTableName} set
                      [StringKey] = {SQLParameterHelper.KeyParameter},
                      [LocalizationTypeId] = {SQLParameterHelper.GroupParameter},
                      {string.Join(",", model.Localizations.Select(i => $"[{i.Locale }] = @{i.Locale}"))}
                       where [Id] = {SQLParameterHelper.IdParameter}";

      var parameters = GetParameters(model);
      parameters.Add(SQLParameterHelper.IdParameter, model.Id);

      await GetConnection().QueryAsync(sql, parameters);
      return model;
    }

    public async override Task<long> DeleteAsync(ILocalizationString model)
    {
      var sql = $@"delete FROM {_tableNamingOptions.LocalizationStringsTableName} where [Id] = {SQLParameterHelper.IdParameter}";

      var parameters = GetParameters(model);
      parameters.Add(SQLParameterHelper.IdParameter, model.Id);

      await GetConnection().QueryAsync(sql, parameters);
      return model.Id;
    }

    public override async Task<IEnumerable<ILocalizationString>> GetAllAsync()
    {
      var localesForQuery = _tableNamingOptions.Locales.Select(i => $"strings.[{i}] as [{i}]").ToList();

      var sql = string.Format(query, string.Join(",", localesForQuery), _tableNamingOptions.LocalizationStringsTableName, _tableNamingOptions.LocalizationGroupsTableName);
      var results = await GetConnection().QueryAsync(sql);
      var models = results.Select(i => (ILocalizationString)GetLocalizationString(i)).ToList();
      return models;
    }

    public override async Task<ILocalizationString> GetByIdAsync(long id)
    {
      var localesForQuery = _tableNamingOptions.Locales.Select(i => $"strings.[{i}] as [{i}]").ToList();

      var sql = string.Format(query, string.Join(",", localesForQuery), _tableNamingOptions.LocalizationStringsTableName, _tableNamingOptions.LocalizationGroupsTableName);
      var result = await GetConnection().QueryFirstAsync($"{sql} where strings.[Id] = {id}");
      return GetLocalizationString(result);
    }

    public async Task<IReadOnlyCollection<ILocalizationString>> GetByKeysAsync(params LocalizationGroupKeyDto[] keys)
    {
      if (keys.Length == 0)
        return Enumerable.Empty<ILocalizationString>().ToList();

      var localesForQuery = _tableNamingOptions.Locales.Select(i => $"strings.[{i}] as [{i}]").ToList();

      var sql = string.Format(query, string.Join(",", localesForQuery), _tableNamingOptions.LocalizationStringsTableName, _tableNamingOptions.LocalizationGroupsTableName);

      var sb = new StringBuilder();

      sb.Append($"(strings.[StringKey] = '{keys[0].Key}' and groups.[Name] = '{keys[0].Group}')");
      foreach (var item in keys.Skip(1))
      {
        sb.Append($" or (strings.[StringKey] = '{item.Key}' and groups.[Name] = '{item.Group}')");
      }

      var result = await GetConnection().QueryAsync($"{sql} where {sb}");
      return result.Select(GetLocalizationString).ToList();
    }

    public async Task<IReadOnlyCollection<ILocalizationString>> GetByIdsAsync(IList<long> sourceIds)
    {
      if (sourceIds.Count == 0)
        return Enumerable.Empty<ILocalizationString>().ToList();

      var localesForQuery = _tableNamingOptions.Locales.Select(i => $"strings.[{i}] as [{i}]").ToList();

      var sql = string.Format(query, string.Join(",", localesForQuery), _tableNamingOptions.LocalizationStringsTableName, _tableNamingOptions.LocalizationGroupsTableName);

      var sb = new StringBuilder();

      sb.Append($"strings.[Id] = '{sourceIds[0]}'");
      foreach (var item in sourceIds.Skip(1))
      {
        sb.Append($" or strings.[Id] = '{item}'");
      }

      var result = await GetConnection().QueryAsync($"{sql} where {sb}");
      return result.Select(GetLocalizationString).ToList();
    }

    private ILocalizationString GetLocalizationString(dynamic result)
    {
      var localizationGroup = new LocalizationGroup(result.GroupId, result.GroupName);
      var resultedLocales = _tableNamingOptions.Locales.Select(i => (ILocalizationPair)GetLocalizationPair(result, i)).ToList();
      var model = new LocalizationStringModel(result.StringId, localizationGroup, result.Key, resultedLocales);
      return model;
    }

    private static ILocalizationPair GetLocalizationPair(dynamic result, string locale)
    {
      var dictionary = result as IDictionary<string, object>;
      var localeStroing = dictionary[locale] as string;
      return new LocalizationPair(locale, localeStroing);
    }

    private static DynamicParameters GetParameters(ILocalizationString model)
    {
      var parameters = new DynamicParameters();

      foreach (var item in model.Localizations)
      {
        parameters.Add($"@{item.Locale}", item.Value);
      }

      parameters.Add(SQLParameterHelper.GroupParameter, model.Group.Id);
      parameters.Add(SQLParameterHelper.KeyParameter, model.Key);
      return parameters;
    }
  }
}