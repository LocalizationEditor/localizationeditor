namespace LocalizationEditor.Base.Extensions
{
  public interface IHostingOption
  {
    bool IsDev { get; }
    bool IsStage { get; }
    bool IsProd { get; }
  }
}