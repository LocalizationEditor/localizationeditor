using MediatR;

namespace LocalizationEditor.BAL.Commands.Requests
{
  public interface IDeleteLocalizationStringRequest : IRequest<long>
  {
    long Id { get; }
  }
}