using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Domain.Entities.Base;

namespace LocalizationEditor.Base.DataLayer
{
  public class IdNameEntity : IdEntity, INameEntity
  {
    [Column("Name")]
    public string Name { get; set; }
  }
}