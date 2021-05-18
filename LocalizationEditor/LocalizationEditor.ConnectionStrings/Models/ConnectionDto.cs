using System;

namespace LocalizationEditor.ConnectionStrings.Models
{
  public class ConnectionDto : IConnection
  {
    public Guid Id { get; private set; }
    public string ConnectionName { get; private set; }
    public string Server { get; private set; }
    public string DbName { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public DbType DataBaseType { get; private set; }
    public bool ForAll { get; private set; }

    public ConnectionDto(
      Guid id,
      string server,
      string dbName,
      string userName,
      string password,
      DbType dataBaseType,
      string connectionName,
      bool forAll)
    {
      Id = id == Guid.Empty ? Guid.NewGuid() : id;
      Server = server;
      DbName = dbName;
      UserName = userName;
      Password = password;
      DataBaseType = dataBaseType;
      ConnectionName = connectionName;
      ForAll = forAll;
    }

    public void Update(IConnection connection)
    {
      ConnectionName = connection.ConnectionName;
      Server = connection.Server;
      DbName = connection.DbName;
      UserName = connection.UserName;
      Password = connection.Password;
      DataBaseType = connection.DataBaseType;
      ForAll = connection.ForAll;
    }
  }
}