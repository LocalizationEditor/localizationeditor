namespace LocalizationEditor.ConnectionStrings.Models
{
  public interface IConnection
  {
    long Id { get; }
    string ConnectionName { get; }
    string Server { get; }
    string DbName { get; }
    string UserName { get; }
    string Password { get; }
    DbType DataBaseType { get; }

    void UpdateId(long id);
    void Update(string connectionName, string server, string dbName, string user, string pass, DbType type);
  }
}