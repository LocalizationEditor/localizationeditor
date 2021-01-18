using LocalizationEditor.BAL.Commands.Requests;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class UpdateLocalizationStringRequest : IUpdateLocalizationStringRequest
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