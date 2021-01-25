using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class UpdateLocalizationStringRequest : IRequest<ILocalizationString>
  {
    public UpdateLocalizationStringRequest(long id, ILocalizationString localizationString)
    {
      Id = id;
      LocalizationString = localizationString;
    }
    public long Id { get; }
    public ILocalizationString LocalizationString { get; }
  }
}