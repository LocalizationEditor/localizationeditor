namespace LocalizationEditor.ConnectionStrings.Models
{
  public interface IConnection
  {
    string ConnectionName { get; }
    string Server { get; }
    string DbName { get; }
    string UserName { get; }
    string Password { get; }
    DbType DataBaseType { get; }
  }
}