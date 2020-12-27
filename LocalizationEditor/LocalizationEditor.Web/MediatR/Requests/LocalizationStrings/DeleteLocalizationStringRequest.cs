using LocalizationEditor.BAL.Commands.Requests;

namespace Localization.MediatR.Requests.LocalizationStrings
{
  public class DeleteLocalizationStringRequest : IDeleteLocalizationStringRequest
  {
    public DeleteLocalizationStringRequest(long id)
    {
      Id = id;
    }

    public long Id { get; }
  }
}