using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using MediatR;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationStringRequest : IRequest<IEnumerable<ILocalizationString>>
  {
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string Search { get; set; }
    public IConnection Connection { get; set; }
  }
}