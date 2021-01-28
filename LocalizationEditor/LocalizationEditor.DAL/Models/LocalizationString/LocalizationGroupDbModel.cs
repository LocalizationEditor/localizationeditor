using LocalizationEditor.Base.Models;

namespace LocalizationEditor.DAL.Models.LocalizationString
{
  public class LocalizationGroupDbModel : IIdentityModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
  }
}