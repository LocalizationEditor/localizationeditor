using System.Collections;
using System.Collections.Generic;

namespace LocalizationEditor.BAL.Configurations
{
  public interface ITablesConfigurationOptions
  {
    string LocalizationGroupsTableName { get; }
    string LocalizationStringsTableName { get; }
    IEnumerable<string> Locales { get; }
  }
}