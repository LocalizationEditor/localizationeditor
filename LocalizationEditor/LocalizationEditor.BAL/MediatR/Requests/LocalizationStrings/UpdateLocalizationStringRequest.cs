using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class UpdateLocalizationStringRequest : IUpdateLocalizationStringRequest
  {
    public UpdateLocalizationStringRequest(long id, ILocalizationKey localizationKey)
    {
      Id = id;
      LocalizationKey = localizationKey;
    }
    public long Id { get; }
    public ILocalizationKey LocalizationKey { get; }
  }
}