using LocalizationEditor.BAL.Commands.Requests;

namespace LocalizationEditor.Web.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationStringRequest : IGetAllLocalizationStringRequest
  {
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string Search { get; set; }
  }
}