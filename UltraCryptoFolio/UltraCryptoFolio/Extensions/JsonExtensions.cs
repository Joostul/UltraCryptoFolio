using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace UltraCryptoFolio.Extensions
{
    public static class JsonExtensions
    {
        public static TResult GetPropertyValue<TResult>(this JObject obj, string propertyName, TResult defaultValue = default(TResult))
        {
            obj = obj ?? throw new ArgumentNullException(nameof(obj));
            propertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

            JProperty prop = obj.Properties().FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
            return prop == null ? defaultValue : (TResult)Convert.ChangeType(prop.Value, typeof(TResult));
        }
    }
}
