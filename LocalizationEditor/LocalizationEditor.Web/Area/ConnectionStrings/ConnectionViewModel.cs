using Newtonsoft.Json;

namespace LocalizationEditor.Web.Area.ConnectionStrings
{
  public class ConnectionViewModel
  {
    [JsonProperty("id")]
    public long Id { get; set; }
    [JsonProperty("connectionName")]
    public string ConnectionName { get; set; }
    [JsonProperty("serverName")]
    public string ServerName { get; set; }
    [JsonProperty("dbName")]
    public string DbName { get; set;}
    [JsonProperty("userName")]
    public string UserName { get; set;}
    [JsonProperty("password")]
    public string Password { get; set;}
    [JsonProperty("dbType")]
    public ConnectionDbTypeViewModel DbType { get; set;}
  }
}