using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationStringRequest : IRequest<IEnumerable<ILocalizationKey>>
  {
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string Search { get; set; }
  }
}