using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.Web.MediatR.Requests.LocalizationStrings
{
  public class AddLocalizationStringRequest : IAddLocalizationStringRequest
  {
    public AddLocalizationStringRequest(ILocalizationRow localizationString)
    {
      LocalizationString = localizationString;
    }
    public ILocalizationRow LocalizationString { get; }
  }
}