using Dapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.BAL.Repositories;
// using LocalizationEditor.DAL.DbContexts;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizationEditor.DAL.Repository.LocalizationString
{
  internal class LocalizationStringRepository : SqlSeverDapperDao<ILocalizationString>, ILocalizationStringRepository
  {
    public LocalizationStringRepository()
      : base(@"Server=slukashov\sqlexpress;User=prockstest;Database=LocalizationEditor;Password=F@mj8p2*~I0WZyRj;")
    {
    }

    public async override Task<ILocalizationString> AddAsync(ILocalizationString localizationString)
    {
      var columns = string.Join(",", localizationString.Localizations.Select(i => i.Locale));
      var columnValues = string.Join(",", localizationString.Localizations.Select(i => i.Value));
      var tableName = "LocalizationStrings";
      var sql = $@"insert {tableName} (Key,GroupId,{columns}) values ({localizationString.Key},{localizationString.Group.Id},{columnValues})";
      await using var connection = new SqlConnection(ConnectionString);
      var newId = await connection.QueryAsync<ILocalizationString>(sql);
      return localizationString;
    }

    public override Task<IEnumerable<ILocalizationString>> GetAllAsync()
    {
      ILocalizationString item =
        new BAL.Models.LocalizationString.Implementations.LocalizationString(0,
          new LocalizationGroup(0, "group"),
          "key",
          new List<ILocalizationPair>
          {
            new LocalizationPair("ru", "sdfgsdfg"),
            new LocalizationPair("ua", "@222"),
            new LocalizationPair("en", "!11"),
          });

      var items = Enumerable.Repeat(item, 100);
      return Task.FromResult(items);
      // return _dao.GetAllAsync();
    }
  }
}