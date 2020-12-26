using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace Localization.MediatR.Requests.LocalizationStrings
{
  public class UpdateLocalizationStringRequest : IUpdateLocalizationStringRequest
  {
    public UpdateLocalizationStringRequest(long id, ILocalizationRow localizationString)
    {
      Id = id;
      LocalizationString = localizationString;
    }
    public long Id { get; }
    public ILocalizationRow LocalizationString { get; }
  }
}