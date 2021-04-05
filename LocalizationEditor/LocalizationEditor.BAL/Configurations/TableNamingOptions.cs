using System.Collections.Generic;

namespace LocalizationEditor.BAL.Configurations
{
  public class TableNamingOptions : ITablesConfigurationOptions
  {
    public string LocalizationStringsTableName { get; set; } = "[CORE_Localization_Strings]";
    public string LocalizationGroupsTableName { get; set; } = "[CORE_Localization_Type]";
    public IEnumerable<string> Locales { get; set; } = new[] { "TextEn", "TextRu", "TextUa" };
  }
}