using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationStringRequest : IRequest<IEnumerable<ILocalizationString>>
  {
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string Search { get; set; }
  }
}