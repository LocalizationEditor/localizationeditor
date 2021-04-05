using System.Collections.Generic;

namespace LocalizationEditor.BAL.Commands.RequestsHandlers.LocalizationStrings
{
  internal interface IDataBaseSettingsProvider
  {
    IEnumerable<string> GetLocalizedColumnNames();
  }
}