using System.ComponentModel;

namespace UltraCryptoFolio.Models
{
    public class Value
    {
        public Currency Currency { get; set; }
        public long Amount { get; set; }
        [Description("Monetary value in MonetaryCurrency in cents.")]
        public long MonetaryValue { get; set; }
    }
}
