using System.ComponentModel;

namespace UltraCryptoFolio.Models
{
    public class CryptoValue
    {
        public CryptoCurrency CryptoCurrency { get; set; }
        public long Amount { get; set; }
        public decimal MonetaryValue { get; set; }
    }
}
