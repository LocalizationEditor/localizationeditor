using Dapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LocalizationEditor.DAL.Repository.LocalizationString
{
  public class SqlSeverDapperDao<T>
  {
    protected string ConnectionString;

    public SqlSeverDapperDao(string connectionString)
    {
      ConnectionString = connectionString;
    }
    public virtual async Task<T> AddAsync(T model)
    {
      var newId = await GetConnection().InsertAsync(model);
      return model;
    }

    public virtual async Task<T> UpdateAsync(T model)
    {
      await GetConnection().UpdateAsync(model);
      return model;
    }

    public virtual Task<T> GetByIdAsync(long id)
    {
      return GetConnection().GetAsync<T>(id);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
      return GetConnection().GetListAsync<T>();
    }

    public virtual async Task<long> DeleteAsync(ILocalizationString localizationString)
    {
      return await GetConnection().DeleteAsync(localizationString);
    }

    protected SqlConnection GetConnection()
    {
      return new SqlConnection(ConnectionString);
    }
  }
}