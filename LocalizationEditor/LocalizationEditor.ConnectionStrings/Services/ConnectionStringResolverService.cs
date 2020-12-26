using System;
using LocalizationEditor.ConnectionStrings.Models;

namespace LocalizationEditor.ConnectionStrings.Services
{
  internal class ConnectionStringResolverService 
    : IConnectionStringResolverService
  {
    public DataBaseConnectionResolver GetConnectionResolver(IConnection connection)
    {
      switch (connection.DataBaseType)
      {
        case DbType.SqlServer:
          return new MssqlConnectionResolver(connection);
        
        default:
          throw new ArgumentException(nameof(connection.DataBaseType));
      }
    }
  }
}