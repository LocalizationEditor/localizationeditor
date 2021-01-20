using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class AddLocalizationStringRequest : IRequest<ILocalizationKey> //IAddLocalizationStringRequest
  {
    public AddLocalizationStringRequest(ILocalizationKey localizationKey)
    {
      LocalizationKey = localizationKey;
    }
    public ILocalizationKey LocalizationKey { get; }
  }
}