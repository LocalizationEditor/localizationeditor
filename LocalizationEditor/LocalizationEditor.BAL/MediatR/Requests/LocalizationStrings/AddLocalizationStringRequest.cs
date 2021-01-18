using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class AddLocalizationStringRequest : IRequest<ILocalizationString> //IAddLocalizationStringRequest
  {
    public AddLocalizationStringRequest(ILocalizationString localizationString)
    {
      LocalizationString = localizationString;
    }
    public ILocalizationString LocalizationString { get; }
  }
}