
using System;

namespace UltraCryptoFolio.Models
{
    public class MonetaryValue
    {
        private decimal _amount;
        public Currency Currency { get; set; }
        public decimal Amount
        {
            get
            {
                return Math.Round(_amount, 2);
            }
            set
            {
                _amount = value;
            }
        }
    }
}
