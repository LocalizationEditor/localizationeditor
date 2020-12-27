namespace LocalizationEditor.ConnectionStrings.Models
{
  public class ConnectionDto : IConnection
  {
    public long Id { get; private set; }
    public string ConnectionName { get; private set; }
    public string Server { get; private set; }
    public string DbName { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public DbType DataBaseType { get; private set; }

    public ConnectionDto(
      long id,
      string server,
      string dbName,
      string userName,
      string password,
      DbType dataBaseType,
      string connectionName)
    {
      Id = id;
      Server = server;
      DbName = dbName;
      UserName = userName;
      Password = password;
      DataBaseType = dataBaseType;
      ConnectionName = connectionName;
    }

    public void UpdateId(long id)
    {
      Id = id;
    }

    public void Update(string connectionName, string server, string dbName, string user, string pass, DbType type)
    {
      ConnectionName = connectionName;
      Server = server;
      DbName = dbName;
      UserName = user;
      Password = pass;
      DataBaseType = type;
    }
  }
}