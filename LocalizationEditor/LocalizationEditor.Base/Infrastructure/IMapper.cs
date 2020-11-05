namespace LocalizationEditor.Base.Infrastructure
{
  public interface IMapper<TFirst, TSecond>
    where TFirst : class
    where TSecond : class
  {
    TFirst GetModel(TSecond second);
    TSecond GetModel(TFirst first);
  }
}