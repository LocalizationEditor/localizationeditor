using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class AddLocalizationStringRequest : IRequest<ILocalizationString> 
  {
    public AddLocalizationStringRequest(ILocalizationString localizationString, IConnection connection)
    {
      LocalizationString = localizationString;
      Connection = connection;
    }
    public ILocalizationString LocalizationString { get; }
    public IConnection Connection { get; }
  }
}