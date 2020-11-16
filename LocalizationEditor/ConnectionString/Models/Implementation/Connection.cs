namespace ConnectionString.Models.Implementation
{
  internal class Connection : IConnection
  {
    public long Id { get; }
    public string Name { get; private set; }
    public string ConnectionString { get; private set; }

    public Connection(
      long id,
      string name,
      string connectionString)
    {
      Id = id;
      Name = name;
      ConnectionString = connectionString;
    }

    public void Update(IConnection connection)
    {
      Name = connection.Name;
      ConnectionString = connection.ConnectionString;
    }
  }
}