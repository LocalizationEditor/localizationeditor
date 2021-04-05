using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.Users
{
  public class LoginViewModel
  {
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("email")]
    public string Password { get; set; }
  }
}
