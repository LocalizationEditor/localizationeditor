using Dapper;
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
    public LocalizationGroupRepository()
      : base(@"Server=slukashov\sqlexpress;User=prockstest;Database=RocksTestV3;Password=F@mj8p2*~I0WZyRj;")
    {
    }

    public async override Task<ILocalizationGroup> AddAsync(ILocalizationGroup model)
    {
      var dbModel = new LocalizationGroupDbModel
      {
        Id = model.Id,
        Name = model.Name
      };

      var newId = await GetConnection().InsertAsync(dbModel);
      model = new LocalizationGroup((long)newId, model.Name);
      return model;
    }

    public async override Task<ILocalizationGroup> UpdateAsync(ILocalizationGroup model)
    {
      var dbModel = new LocalizationGroupDbModel
      {
        Id = model.Id,
        Name = model.Name
      };

      await GetConnection().UpdateAsync(dbModel);
      return model;
    }

    public override async Task<IEnumerable<ILocalizationGroup>> GetAllAsync()
    {
      var results = await GetConnection().GetListAsync<LocalizationGroupDbModel>();
      var models = results.Select(i => new LocalizationGroup(i.Id, i.Name)).ToList();
      return models;
    }

    public async Task<ILocalizationGroup> SearchByGroupKeyAsync(string groupKey)
    {
      var result = await GetConnection().QueryFirstOrDefaultAsync<LocalizationGroupDbModel>($"select * from [CORE_Localization_Type] where Name = '{groupKey}'");
      if (result is null)
        return null;
      
      return new LocalizationGroup(result.Id, result.Name);

    }
  }
}