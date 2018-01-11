using Newtonsoft.Json;
using System;

namespace UltraCryptoFolio.Extensions
{
    public abstract class AbstractJsonConverter<Transaction> : JsonConverter
    {
        public JsonSerializer Serializer { get; set; }

        private readonly bool _shouldPerformObjectCheck;
        protected abstract Transaction Read(JsonReader reader);
        protected abstract void Write(JsonWriter writer, Transaction value);
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Transaction);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Serializer = serializer;
            if (_shouldPerformObjectCheck && reader.TokenType != JsonToken.StartObject)
            {
                return null;
            }
            return Read(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Serializer = serializer;
            if (CanConvert(value.GetType()))
            {
                Write(writer, (Transaction)value);
            }
        }
    }
}
