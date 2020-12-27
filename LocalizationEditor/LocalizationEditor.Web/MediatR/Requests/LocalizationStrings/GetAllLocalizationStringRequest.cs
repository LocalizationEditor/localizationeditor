using LocalizationEditor.BAL.Commands.Requests;

namespace Localization.MediatR.Requests.LocalizationStrings
{
  public class GetAllLocalizationStringRequest : IGetAllLocalizationStringRequest
  {
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string Search { get; set; }
  }
}