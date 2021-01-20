using LocalizationEditor.BAL.Models.LocalizationString;
using MediatR;

namespace LocalizationEditor.BAL.Commands.Requests
{
  public interface IUpdateLocalizationStringRequest : IRequest<ILocalizationKey>
  {
    long Id { get; }
    ILocalizationKey LocalizationKey { get; }
  }
}