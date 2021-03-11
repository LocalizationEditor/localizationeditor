using Newtonsoft.Json;
using System.Collections.Generic;

namespace LocalizationEditor.Web.Area.Syncronize.Merge
{
  public class SelectedMergeViewModel : MergeConnectionIdView
  {
    [JsonProperty("localizationIds")]
    public IReadOnlyCollection<long> SourceLocalizationIds { get; set; }
  }
}
