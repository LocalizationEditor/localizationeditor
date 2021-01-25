using Dapper;
using LocalizationEditor.Base.Models;

namespace LocalizationEditor.DAL.Models.LocalizationString
{
  [Table("CORE_Localization_Type")]
  public class LocalizationGroupDbModel : IIdentityModel
  {
    public long Id { get; set; }
    public string Name { get; set; }
  }
}