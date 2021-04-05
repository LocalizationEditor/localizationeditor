using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.ViewModels.Merge
{
  public class SelectedMergeViewModel : MergeConnectionIdView
  {
    [JsonProperty("localizationIds")]
    public IReadOnlyCollection<long> SourceLocalizationIds { get; set; }
  }
}
