using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetOrAddLocalizationGroupRequest : IRequest<ILocalizationGroup>
  {
    public GetOrAddLocalizationGroupRequest(string groupName)
    {
      GroupName = groupName;
    }
    public string GroupName { get; }
    public string ConnectionStringKey { get; set; }
  }
}