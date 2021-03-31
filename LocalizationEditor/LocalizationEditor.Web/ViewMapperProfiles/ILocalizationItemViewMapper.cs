using LocalizationEditor.BAL.Models.LocalizationString;
using LocalizationEditor.ConnectionStrings.Models;
using LocalizationEditor.Web.ViewModels.LocalizationStrings;
using System.Threading.Tasks;

namespace LocalizationEditor.Web.ViewMapperProfiles
{
  public interface ILocalizationItemViewMapper
  {
    Task<ILocalizationString> GetDomain(LocalizationStringItemView view, IConnection connection);
    LocalizationStringItemView GetView(ILocalizationString model);
  }
}