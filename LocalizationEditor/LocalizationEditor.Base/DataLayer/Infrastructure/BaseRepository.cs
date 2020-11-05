using System.Threading.Tasks;
using Domain.Entities.Base;
using LocalizationEditor.Base.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace LocalizationEditor.Base.DataLayer.Infrastructure
{
  public abstract class BaseRepository<TDomain, TDb> : IBaseRepository<TDomain>
    where TDb : class, IIdEntity
    where TDomain : class, IIdEntity
  {
    protected readonly DbContext Context;
    protected  readonly IMapper<TDomain, TDb> Mapper;
    protected readonly DbSet<TDb> Set;
    
    public BaseRepository(
      DbContext context, 
      IMapper<TDomain, TDb> mapper)
    {
      Context = context;
      Mapper = mapper;
      Set = Context.Set<TDb>();
    }

    public async Task<TDomain> AddAsync(TDomain domain)
    {
      var db = Mapper.GetModel(domain);
      var model = await Context.AddAsync(db);
      await Context.SaveChangesAsync();
      return Mapper.GetModel(model.Entity);
    }

    public async Task<TDomain> GetByIdAsync(long id)
    {
      var model = await Set.FirstOrDefaultAsync(i => i.Id == id);
      return Mapper.GetModel(model);
    }

    public async Task UpdateAsync(TDomain domain)
    {
      var db = Mapper.GetModel(domain);
      Context.Update(db);
      await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TDomain domain)
    {
      var db = Mapper.GetModel(domain);
      Context.Remove(db);
      await Context.SaveChangesAsync();
    }
  }
}