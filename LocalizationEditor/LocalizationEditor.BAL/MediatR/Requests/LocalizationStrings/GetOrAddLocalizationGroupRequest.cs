using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetOrAddLocalizationGroupRequest : IRequest<ILocalizationGroup>
  {
    public GetOrAddLocalizationGroupRequest(string groupName, IConnection connection)
    {
      GroupName = groupName;
      Connection = connection;
    }
    public string GroupName { get; }
    public IConnection Connection { get; }
  }
}