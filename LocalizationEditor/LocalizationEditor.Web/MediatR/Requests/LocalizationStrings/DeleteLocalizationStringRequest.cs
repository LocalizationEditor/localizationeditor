using LocalizationEditor.BAL.Commands.Requests;

namespace LocalizationEditor.Web.MediatR.Requests.LocalizationStrings
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