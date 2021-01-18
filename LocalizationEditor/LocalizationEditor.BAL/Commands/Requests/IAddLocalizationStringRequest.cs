using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.Commands.Requests
{
  public interface IAddLocalizationStringRequest : IRequest<ILocalizationString>
  { 
    ILocalizationString LocalizationString { get; }
  }
}