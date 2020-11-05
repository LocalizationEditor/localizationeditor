using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Auth.Domain.Models
{
  [Table("AUTH_UserData")]
  public class DbUserAuth : IdEntity
  {
    [Column("Email")]
    public string Email { get; set; }
    [Column("Password")]
    public string Password { get; set; }
  }
}