using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using MediatR;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationGroupsRequest : IRequest<IEnumerable<ILocalizationGroup>>
  {
    public IConnection Connection { get; }

    public GetAllLocalizationGroupsRequest(IConnection connection)
    {
      Connection = connection;
    }
  }
}