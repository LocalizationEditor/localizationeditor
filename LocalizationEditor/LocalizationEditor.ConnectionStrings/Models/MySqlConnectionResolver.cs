using System;

namespace LocalizationEditor.ConnectionStrings.Models
{
  internal class MySqlConnectionResolver : IDataBaseConnectionResolver
  {
    public DbType DatabaseType => DbType.MySql;

    public bool CanHandle(IConnection connection)
    {
      return connection.DataBaseType == DatabaseType;
    }
   
    public string GetConnectionString(IConnection connection)
    {
      throw new NotImplementedException();
    }
  }
}