using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LocalizationEditor.Base.Utils.Json
{
  public class JsonSensitiveValueProvider : IValueProvider
  {
    private readonly Coder _coder;
    private readonly Func<object, object> _getter;
    private readonly Action<object, object> _setter;
    private readonly Type _memberType;
    
    public JsonSensitiveValueProvider(MemberInfo info, Coder coder)
    {
      _coder = coder;
      if (info is PropertyInfo propertyInfo)
      {
        _getter = propertyInfo.GetValue;
        _setter = propertyInfo.SetValue;
        _memberType = propertyInfo.PropertyType;
      }
    }
    
    public void SetValue(object target, object value)
    {
      var decryptText = _coder.Encrypt(value as string);
      _setter.Invoke(target, decryptText);
    }

    public object GetValue(object target)
    {
      var value = _getter.Invoke(target);
      var json = JsonConvert.SerializeObject(value);
      return _coder.Decrypt(json);
    }
  }
}