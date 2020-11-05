using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace LocalizationEditor.Base.DataLayer.Infrastructure
{
  public abstract class BaseContext<TDb> : DbContext
    where TDb : class, IIdEntity
  {
    private readonly string _connectionString;
    public DbSet<TDb> DbModel { get; set; }

    public BaseContext(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder
        .UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<TDb>()
        .HasKey(i => i.Id);
    }
  }
}