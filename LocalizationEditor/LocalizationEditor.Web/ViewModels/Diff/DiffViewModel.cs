using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewModels.Diff
{
  public class DiffViewModel
  {
    [JsonProperty("destinations")]
    public IReadOnlyCollection<LocalizationStringItemView> Destinations { get; set; }
    [JsonProperty("sources")]
    public IReadOnlyCollection<LocalizationStringItemView> Sources { get; set; }
  }
}
