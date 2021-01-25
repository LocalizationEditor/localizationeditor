using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class DeleteLocalizationStringRequest : IRequest<long>
  {
    public DeleteLocalizationStringRequest(long id)
    {
      Id = id;
    }

    public long Id { get; }
  }

  public class SearchLocalizationStringRequest : IRequest<ILocalizationString>
  {
    public SearchLocalizationStringRequest(string groupKey, string localizationStringKey)
    {
      GroupKey = groupKey;
      LocalizationStringKey = localizationStringKey;
    }

    public string GroupKey { get; }
    public string LocalizationStringKey { get; }
  }
}