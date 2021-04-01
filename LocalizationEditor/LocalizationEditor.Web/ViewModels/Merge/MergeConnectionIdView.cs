using System;
using Newtonsoft.Json;

namespace LocalizationEditor.Web.ViewModels.Merge
{
  public class MergeConnectionIdView
  {
    [JsonProperty("sourceId")]
    public Guid SourceId { get; set; }
    [JsonProperty("destinationId")]
    public Guid DestinationId { get; set; }
  }
}
