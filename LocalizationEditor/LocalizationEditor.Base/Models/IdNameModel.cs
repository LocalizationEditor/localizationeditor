using Domain.Entities.Base;

namespace LocalizationEditor.Base.Models
{
  public class IdNameModel : IIdEntity, INameEntity
  {
    public long Id { get; }
    public string Name { get; }
    
    public IdNameModel(long id, string name)
    {
      Id = id;
      Name = name;
    }
  }
}