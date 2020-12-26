using LocalizationEditor.ConnectionStrings.Models;
using Newtonsoft.Json;

namespace Localization.Area.ConnectionStrings
{
  public class ConnectionViewModel
  {
    [JsonProperty("connectionName")]
    public string ConnectionName { get; set; }
    [JsonProperty("serverName")]
    public string Server { get; set; }
    [JsonProperty("dbName")]
    public string DbName { get; set;}
    [JsonProperty("userName")]
    public string UserName { get; set;}
    [JsonProperty("password")]
    public string Password { get; set;}
    [JsonProperty("dbType")]
    public DbType DataBaseType { get; set;}
  }
}