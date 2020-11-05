using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LocalizationEditor.Base.Utils.Json
{
  public class JsonSensitiveContractResolver : CamelCasePropertyNamesContractResolver
  {
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      var property = base.CreateProperty(member, memberSerialization);

      if (member.CustomAttributes
        .Any(i => i.AttributeType == typeof(SensitivePropertyAttribute)))
      {
        property.ValueProvider = new JsonSensitiveValueProvider(member, new Coder());
        property.PropertyType = typeof(string);
      }

      return property;
    }
  }
}