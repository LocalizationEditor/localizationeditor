using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.Commands.Requests
{
  public interface IUpdateLocalizationStringRequest : IRequest<ILocalizationString>
  {
    long Id { get; }
    ILocalizationString LocalizationString { get; }
  }
}