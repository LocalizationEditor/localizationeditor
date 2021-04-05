using LocalizationEditor.Web.ViewModels.Base;
using Newtonsoft.Json;

namespace LocalizationEditor.Web.Controllers.ConnectionStrings
{
  public class IdNamePairView : IdView
  {
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}