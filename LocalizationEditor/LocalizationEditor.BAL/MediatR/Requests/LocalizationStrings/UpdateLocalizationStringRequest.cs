using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class UpdateLocalizationStringRequest : IRequest<ILocalizationString>
  {
    public UpdateLocalizationStringRequest(long id, ILocalizationString localizationString, IConnection connection)
    {
      Id = id;
      LocalizationString = localizationString;
      Connection = connection;
    }
    public long Id { get; }
    public ILocalizationString LocalizationString { get; }
    public IConnection Connection { get; }
  }
}