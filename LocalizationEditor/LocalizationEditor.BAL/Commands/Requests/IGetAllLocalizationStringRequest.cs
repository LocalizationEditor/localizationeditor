using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.Commands.Requests
{
  public interface IGetAllLocalizationStringRequest : IRequest<IEnumerable<ILocalizationKey>>
  {
    int Limit { get; set; }
    int Offset { get; set; }
    string Search { get; set; }
  }
}