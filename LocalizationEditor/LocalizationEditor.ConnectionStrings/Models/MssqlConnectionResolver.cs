﻿namespace LocalizationEditor.ConnectionStrings.Models
{
  internal class MssqlConnectionResolver : DataBaseConnectionResolver
  {
    public MssqlConnectionResolver(IConnection connection) 
      : base(connection)
    {
    }

    public override string GetConnectionString()
    {
      return $"Server={Connection.Server};Database={Connection.DbName};User={Connection.UserName};Password={Connection.Password}";
    }
  }
}