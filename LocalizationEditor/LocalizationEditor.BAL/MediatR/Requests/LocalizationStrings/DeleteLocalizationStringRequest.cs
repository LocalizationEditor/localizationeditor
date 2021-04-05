using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using MediatR;

namespace LocalizationEditor.BAL.MediatR.Requests.LocalizationStrings
{
  public class DeleteLocalizationStringRequest : IRequest<long>
  {
    public DeleteLocalizationStringRequest(long id, IConnection connection)
    {
      Id = id;
      Connection = connection;
    }

    public long Id { get; }
    public IConnection Connection { get; }
  }

  public class SearchLocalizationStringRequest : IRequest<ILocalizationString>
  {
    public SearchLocalizationStringRequest(string groupKey, string localizationStringKey, IConnection connection)
    {
      GroupKey = groupKey;
      LocalizationStringKey = localizationStringKey;
      Connection = connection;
    }

    public string GroupKey { get; }
    public string LocalizationStringKey { get; }
    public IConnection Connection { get; }
  }
}