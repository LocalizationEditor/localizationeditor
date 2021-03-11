namespace LocalizationEditor.Base.Extensions
{
  public class HostingOption : IHostingOption
  {
    public bool IsDev { get; }

    public bool IsStage { get; }

    public bool IsProd { get; }

    public HostingOption(bool isProd, bool isStage, bool isDev)
    {
      IsProd = isProd;
      IsStage = isStage;
      IsDev = isDev;
    }
  }
}