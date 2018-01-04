using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Helpers;

namespace UltraCryptoFolio.Models
{
    public class Dividend : Transaction
    {
        [Required]
        [Description("Amount received in satoshi or cents.")]
        public long AmountReceived { get; set; }
        [Required]
        public CryptoCurrency ReceivingCurrency { get; set; }
        public override TransactionType TransactionType => TransactionType.Dividend;
        private decimal _transactionWorth;
        public override decimal TransactionWorth
        {
            get
            {
                if (_transactionWorth == 0m)
                {
                    using (var priceGetter = new PriceGetter())
                    {
                        if (ReceivingCurrency == CryptoCurrency.Stellar)
                        {
                            _transactionWorth = (priceGetter.GetEuroPriceOnDateAsync(ReceivingCurrency, DateTime).Result * AmountReceived);
                        }
                        else
                        {
                            _transactionWorth = ((priceGetter.GetEuroPriceOnDateAsync(ReceivingCurrency, DateTime).Result * AmountReceived) / 100000000);
                        }
                    }
                }
                return Math.Round(_transactionWorth, 2);
            }
        }
    }
}
