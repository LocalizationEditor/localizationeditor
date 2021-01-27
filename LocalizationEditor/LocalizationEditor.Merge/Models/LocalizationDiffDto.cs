using System.Collections.Generic;
using LocalizationEditor.BAL.Models.LocalizationString;

namespace LocalizationEditor.Merge.Models
{
  internal class LocalizationDiffDto
  {
    public IReadOnlyCollection<string> MigrationKey { get; }
    public IReadOnlyCollection<ILocalizationKey> AddKeys { get; }
    public IReadOnlyCollection<ILocalizationKey> RemoveKeys { get; }
    public IReadOnlyCollection<ILocalizationKey> EditKeys { get; }

    public LocalizationDiffDto(
      IReadOnlyCollection<ILocalizationKey> addKeys,
      IReadOnlyCollection<ILocalizationKey> removeKeys,
      IReadOnlyCollection<ILocalizationKey> editKeys,
      IReadOnlyCollection<string> migrationKey = null)
    {
      AddKeys = addKeys;
      RemoveKeys = removeKeys;
      EditKeys = editKeys;
      MigrationKey = migrationKey;
    }
  }
}