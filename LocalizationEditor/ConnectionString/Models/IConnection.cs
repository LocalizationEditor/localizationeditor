using Base.Models;

namespace ConnectionString.Models
{
  public interface IConnection : IIdNameModel
  {
    string ConnectionString { get; }

    void Update(IConnection connection);
  }
}