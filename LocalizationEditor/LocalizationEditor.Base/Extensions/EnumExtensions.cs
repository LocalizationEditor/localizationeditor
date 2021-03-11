using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalizationEditor.Base.Extensions
{
  public static class EnumExtensions
  {
    public static IEnumerable<T> GetValueList<T>()
    {
      return Enum.GetValues(typeof(T)).Cast<T>();
    }
  }
}