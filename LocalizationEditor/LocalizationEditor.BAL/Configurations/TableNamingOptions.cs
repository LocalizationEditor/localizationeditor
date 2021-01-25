namespace LocalizationEditor.BAL.Configurations
{
  public class TableNamingOptions : ITableNamingOptions
  {
    public string LocalizationStringsTableName { get; set; } = "[CORE_Localization_Strings]";
    public string LocalizationGroupsTableName { get; set; } = "[CORE_Localization_Type]";
  }
}