using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.BAL.Repositories;
using Microsoft.Data.SqlClient;

namespace LocalizationEditor.DAL.Repository
{
  public class LocalizationGroupRepository : ILocalizationGroupRepository
  {
    private const string GroupTableName = "Localization_Group";

    public async Task<ILocalizationGroup> AddAsync(ILocalizationGroup localizationGroup)
    {
      using (IDbConnection db = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=LocalizationEditor;Trusted_Connection=True;"))
      {
        var query = $"INSERT INTO {GroupTableName} (Name) VALUES(@Name)";
        await db.ExecuteAsync(query, localizationGroup);
      }

      return localizationGroup;
    }

    public Task<ILocalizationGroup> UpdateAsync(ILocalizationGroup localizationString)
    {
      throw new System.NotImplementedException();
    }

    public Task<ILocalizationGroup> GetByIdAsync(long id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ILocalizationGroup>> GetAllAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task<long> DeleteAsync(ILocalizationGroup localizationString)
    {
      throw new System.NotImplementedException();
    }
  }
}