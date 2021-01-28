using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationGroupsRequest : IRequest<IEnumerable<ILocalizationGroup>>
  {
    public string ConnectionStringKey { get; set; }
  }
}