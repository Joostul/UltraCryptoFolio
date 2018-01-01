using System;

namespace UltraCryptoFolio.Models
{
    public class CryptoValue
    {
        private decimal _monetaryValue;
        public CryptoCurrency CryptoCurrency { get; set; }
        public long Amount { get; set; }
        public decimal MonetaryValue
        {
            get
            {
                return Math.Round(_monetaryValue, 2);
            }
            set
            {
                _monetaryValue = value;
            }
        }
    }
}
