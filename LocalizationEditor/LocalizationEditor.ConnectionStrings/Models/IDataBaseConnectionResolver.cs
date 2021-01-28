namespace LocalizationEditor.ConnectionStrings.Models
{
  public interface IDataBaseConnectionResolver
  {
    DbType DatabaseType { get; }

    string GetConnectionString(IConnection connection);
    bool CanHandle(IConnection connection);
  }
}