using Auth.Domain.Models;
using LocalizationEditor.Base.DataLayer.Infrastructure;
using LocalizationEditor.Base.Utils;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Context
{
  internal class AuthContext : BaseContext<DbUserAuth>
  {
    private readonly IDbCoder _dbCoder;

    public AuthContext(IDbCoder dbCoder)
      : base("Server=(localdb)\\MSSQLLocalDB;Database=LocalizationEditor;Trusted_Connection=True;")
    {
      _dbCoder = dbCoder;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<DbUserAuth>()
        .HasKey(i => i.Id);

      /*modelBuilder.Entity<DbUserAuth>()
        .Property(i => i.Email)
        .HasConversion(
          i => _dbCoder.Encrypt(i),
          i => _dbCoder.Decrypt(i)); //i => i);

      modelBuilder.Entity<DbUserAuth>()
        .Property(i => i.Password)
        .HasConversion(
          i => _dbCoder.Encrypt(i),
          i => _dbCoder.Decrypt(i));*/
    }
  }
}