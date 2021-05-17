using System;
using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.ConnectionStrings
{
  public class ConnectionViewModel
  {
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("connectionName")]
    public string ConnectionName { get; set; }
    [JsonProperty("serverName")]
    public string ServerName { get; set; }
    [JsonProperty("dbName")]
    public string DbName { get; set; }
    [JsonProperty("userName")]
    public string UserName { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("dbType")]
    public ConnectionDbTypeViewModel DbType { get; set; }
    [JsonProperty("forAll")]
    public bool ForAll { get; set; }
  }
}