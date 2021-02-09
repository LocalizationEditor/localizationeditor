using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class AddLocalizationStringRequest : IRequest<ILocalizationString> 
  {
    public AddLocalizationStringRequest(ILocalizationString localizationString)
    {
      LocalizationString = localizationString;
    }
    public ILocalizationString LocalizationString { get; }
    public string ConnectionStringKey { get; set; }
  }
}