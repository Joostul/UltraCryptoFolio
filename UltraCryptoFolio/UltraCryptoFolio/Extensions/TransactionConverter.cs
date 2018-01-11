using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Extensions
{
    public class TransactionConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings()
        {
            ContractResolver = new BaseSpecifiedConcreteClassConverter()
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Transaction));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            TransactionType transactionType = (TransactionType)jo["TransactionType"].Value<int>();
            switch (transactionType)
            {
                case TransactionType.Investment:
                    return JsonConvert.DeserializeObject<Investment>(jo.ToString(), SpecifiedSubclassConversion);
                case TransactionType.Trade:
                    return JsonConvert.DeserializeObject<Trade>(jo.ToString(), SpecifiedSubclassConversion);
                case TransactionType.Spend:
                    return JsonConvert.DeserializeObject<Spend>(jo.ToString(), SpecifiedSubclassConversion);
                case TransactionType.Divestment:
                    return JsonConvert.DeserializeObject<Divestment>(jo.ToString(), SpecifiedSubclassConversion);
                case TransactionType.Dividend:
                    return JsonConvert.DeserializeObject<Dividend>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new InvalidOperationException();
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
