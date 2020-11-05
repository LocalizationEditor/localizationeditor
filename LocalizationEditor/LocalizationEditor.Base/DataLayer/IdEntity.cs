using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Base
{
  public class IdEntity : IIdEntity 
  {
    [Key]
    [Column("Id")]
    public long Id { get; set; }
  }
}