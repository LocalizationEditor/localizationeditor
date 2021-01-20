using System.Data;

namespace LocalizationEditor.ConnectionStrings.Models
{
  public abstract class DataBaseConnectionResolver
  {
    protected readonly IConnection Connection;

    protected DataBaseConnectionResolver(IConnection connection)
    {
      Connection = connection;
    }

    public abstract string GetConnectionString();
    public abstract IDbConnection GetConnection();
  }
}