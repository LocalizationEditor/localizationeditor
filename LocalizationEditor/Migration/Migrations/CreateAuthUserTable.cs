using FluentMigrator;

namespace Migration.Migrations
{
  [Migration(202011260, "a.haptelmanov issue-9")]
  public class CreateAuthUserTable : ForwardOnlyMigration
  {
    public override void Up()
    {
      if(!Schema.Table("AUTH_UserData").Exists())
        Execute.EmbeddedScript("CreateUserAuthTable.sql");
    }
  }
}