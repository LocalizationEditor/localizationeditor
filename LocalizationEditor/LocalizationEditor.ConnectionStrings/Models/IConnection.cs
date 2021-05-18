using System;

namespace LocalizationEditor.ConnectionStrings.Models
{
  public interface IConnection
  {
    Guid Id { get; }
    string ConnectionName { get; }
    string Server { get; }
    string DbName { get; }
    string UserName { get; }
    string Password { get; }
    DbType DataBaseType { get; }
    bool ForAll { get; }

    void Update(IConnection connection);
  }
}