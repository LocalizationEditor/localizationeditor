using System;
using Newtonsoft.Json;

namespace LocalizationEditor.Web.Controllers.ConnectionStrings
{
  public class UserView
  {
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("userName")]
    public string UserName { get; set; }
    [JsonProperty("password")]
    public string Password { get; set; }
    [JsonProperty("role")]
    public IdNamePairView Role { get; set; }
  }
}