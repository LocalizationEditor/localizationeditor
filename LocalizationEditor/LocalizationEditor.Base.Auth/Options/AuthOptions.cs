namespace LocalizationEditor.Base.Auth.Options
{
  internal class AuthOptions : IAuthOptions
  {
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public int TokenLifeTime { get; set; }
  }
}