using LocalizationEditor.BAL.Models.LocalizationString.Implementations;
using LocalizationEditor.Base.Models;
using System.Collections.Generic;

namespace LocalizationEditor.DAL.Models.LocalizationString
{
  public class LocalizationStringDbModel : IIdentityModel
  {
    public long Id { get; set; }
    public string Key { get; set; }
  }
}