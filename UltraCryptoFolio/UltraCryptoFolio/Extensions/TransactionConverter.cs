using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Extensions
{
    public class TransactionConverter : AbstractJsonConverter<Transaction> 
    {
        protected override Transaction Read(JsonReader reader)
        {
            var jsonObject = JObject.Load(reader);

            Transaction transaction = null;
            TransactionType transactionType = JsonExtensions.GetPropertyValue(jsonObject, "TransactionType", TransactionType.Investment);

            switch (transactionType)
            {
                case TransactionType.Investment:
                    transaction = new Investment();
                    break;
                case TransactionType.Trade:
                    transaction = new Trade();
                    break;
                case TransactionType.Spend:
                    transaction = new Spend();
                    break;
                case TransactionType.Divestment:
                    transaction = new Divestment();
                    break;
                case TransactionType.Dividend:
                    transaction = new Dividend();
                    break;
                default:
                    break;
            }

            return transaction;
        }

        protected override void Write(JsonWriter writer, Transaction value)
        {
            throw new NotImplementedException();
        }
    }
}
