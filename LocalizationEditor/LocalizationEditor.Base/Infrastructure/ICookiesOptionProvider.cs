using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizationEditor.Base.Infrastructure
{
  public interface ICookiesOptionProvider
  {
    string Key { get; }
    int Expires { get; }
  }
}
