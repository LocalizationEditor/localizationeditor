using System.Linq;

namespace LocalizationEditor.Base.DapperHelper
{
  public interface IDapperQueryHelper
  {
    string GetSelectQuery(string tableName, string whereExpression = null, string queryParam = null);
  }

  internal class DapperQueryHelper : IDapperQueryHelper
  {
    public string GetSelectQuery(string tableName, string whereExpression = null, string queryParam = null)
    {
      return $"select {queryParam ?? tableName.ToLower()}.* from {tableName} {whereExpression}";
    }
  }
}