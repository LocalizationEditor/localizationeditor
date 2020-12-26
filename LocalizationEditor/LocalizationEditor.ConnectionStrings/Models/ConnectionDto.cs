namespace LocalizationEditor.ConnectionStrings.Models
{
  public class ConnectionDto : IConnection
  {
    public string ConnectionName { get; }
    public string Server { get; }
    public string DbName { get; }
    public string UserName { get; }
    public string Password { get; }
    public DbType DataBaseType { get; }
    
    public ConnectionDto(
      string server, 
      string dbName,
      string userName,
      string password,
      DbType dataBaseType,
      string connectionName)
    {
      Server = server;
      DbName = dbName;
      UserName = userName;
      Password = password;
      DataBaseType = dataBaseType;
      ConnectionName = connectionName;
    }
  }
}