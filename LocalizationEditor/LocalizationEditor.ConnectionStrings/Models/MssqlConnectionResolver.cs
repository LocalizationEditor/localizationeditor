namespace LocalizationEditor.ConnectionStrings.Models
{
  internal class MssqlConnectionResolver : IDataBaseConnectionResolver
  {
    public DbType DatabaseType => DbType.SqlServer;

    public bool CanHandle(IConnection connection)
    {
      return connection.DataBaseType == DatabaseType;
    }
    
    public  string GetConnectionString(IConnection connection)
    {
      return $"Server={connection.Server};Database={connection.DbName};User={connection.UserName};Password={connection.Password}";
    }
  }
}