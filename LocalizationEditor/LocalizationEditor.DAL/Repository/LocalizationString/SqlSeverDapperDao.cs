using Dapper;
using LocalizationEditor.BAL.Models.LocalizationString;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LocalizationEditor.DAL.Repository.LocalizationString
{
  public class SqlSeverDapperDao<T>
  {
    protected readonly string ConnectionString;

    public SqlSeverDapperDao(string connectionString)
    {
      ConnectionString = connectionString;
    }
    public virtual async Task<T> AddAsync(T model)
    {
      await using var connection = new SqlConnection(ConnectionString);
      var newId = await connection.InsertAsync(model);
      return model;
    }

    public virtual async Task<T> UpdateAsync(T model)
    {
      await using var connection = new SqlConnection(ConnectionString);
      await connection.UpdateAsync(model);
      return model;
    }

    public virtual Task<T> GetByIdAsync(long id)
    {
      using var connection = new SqlConnection(ConnectionString);
      return connection.GetAsync<T>(id);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
    {
      using var connection = new SqlConnection(ConnectionString);
      return connection.GetListAsync<T>();
    }

    public virtual async Task<long> DeleteAsync(ILocalizationString localizationString)
    {
      await using var connection = new SqlConnection(ConnectionString);
      return await connection.DeleteAsync(localizationString);
    }
  }
}