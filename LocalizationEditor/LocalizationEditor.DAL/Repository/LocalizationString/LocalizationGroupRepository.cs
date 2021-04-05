using Dapper;
using LocalizationEditor.BAL.Configurations;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
using LocalizationEditor.DAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.DAL.Repository.LocalizationString
{
  internal class LocalizationGroupRepository : SqlServerDapperDao<ILocalizationGroup>, ILocalizationGroupRepository
  {
    private readonly ITablesConfigurationOptions _tableNamingOptions;

    public LocalizationGroupRepository(ITablesConfigurationOptions tableNamingOptions)
    {
      _tableNamingOptions = tableNamingOptions;
    }

    private static DynamicParameters GetParameters(ILocalizationGroup model)
    {
      var parameters = new DynamicParameters { RemoveUnused = true };

      parameters.Add(SqlParameterHelper.IdParameter, model.Id);
      parameters.Add(SqlParameterHelper.NameParameter, model.Name);
      return parameters;
    }

    public async override Task<ILocalizationGroup> AddAsync(ILocalizationGroup model)
    {
      var parameters = GetParameters(model);

      var sql = $@"insert {_tableNamingOptions.LocalizationGroupsTableName} ([Name])
                  values ({SqlParameterHelper.NameParameter})
                  select cast(scope_identity() as int) as Id";
      var result = await GetConnection().QueryFirstAsync(sql, parameters);
      model = new LocalizationGroup((long)result.Id, model.Name);
      return model;
    }

    public void SetConnectionString(string connectionString)
    {
      ConnectionString = connectionString;
    }

    public async override Task<ILocalizationGroup> UpdateAsync(ILocalizationGroup model)
    {
      var parameters = GetParameters(model);
      var sql = $@"update {_tableNamingOptions.LocalizationGroupsTableName} set
                      [Name] = {SqlParameterHelper.NameParameter}
                       where [Id] = {SqlParameterHelper.IdParameter}";
      await GetConnection().QueryAsync(sql, parameters);
      return model;
    }

    public override async Task<IEnumerable<ILocalizationGroup>> GetAllAsync()
    {
      var results = await GetConnection().QueryAsync($@"select * from {_tableNamingOptions.LocalizationGroupsTableName}");
      return results.Select(i => new LocalizationGroup(i.Id, i.Name));
    }

    public async Task<ILocalizationGroup> SearchByGroupKeyAsync(string groupKey)
    {
      var parameters = new DynamicParameters { RemoveUnused = true };
      parameters.Add(SqlParameterHelper.NameParameter, groupKey);

      var sql = $@"select * from {_tableNamingOptions.LocalizationGroupsTableName}
                      where [Name] = {SqlParameterHelper.NameParameter}";
      var result = await GetConnection().QueryFirstOrDefaultAsync<LocalizationGroupDbModel>(sql, parameters);
      if (result is null)
        return null;

      return new LocalizationGroup(result.Id, result.Name);
    }
  }
}