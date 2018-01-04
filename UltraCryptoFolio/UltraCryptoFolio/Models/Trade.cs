using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Helpers;

namespace UltraCryptoFolio.Models
{
    public class Trade : Transaction
    {
        [Required]
        [Description("Amount spent in satoshi or cents.")]
        public long AmountSpent { get; set; }
        [Required]
        public CryptoCurrency SpendingCurrency { get; set; }
        [Required]
        [Description("Amount received in satoshi or cents.")]
        public long AmountReceived { get; set; }
        [Required]
        public CryptoCurrency ReceivingCurrency { get; set; }
        public int? ExchangeRate { get; set; }
        public override TransactionType TransactionType => TransactionType.Trade;
        private decimal _transactionWorth;
        public override decimal TransactionWorth
        {
            get
            {
                if (_transactionWorth == 0m)
                {
                    using (var priceGetter = new PriceGetter())
                    {
                        if (SpendingCurrency == CryptoCurrency.Stellar)
                        {
                            _transactionWorth = (priceGetter.GetEuroPriceOnDateAsync(SpendingCurrency, DateTime).Result * AmountSpent);
                        }
                        else
                        {
                            _transactionWorth = ((priceGetter.GetEuroPriceOnDateAsync(SpendingCurrency, DateTime).Result * AmountSpent) / 100000000);
                        }
                    }
                }
                return Math.Round(_transactionWorth, 2);
            }
        }
    }
}
