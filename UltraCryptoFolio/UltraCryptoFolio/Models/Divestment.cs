using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models
{
    public class Divestment : Transaction
    {
        [Required]
        [Description("Amount spent in satoshi or cents.")]
        public long AmountSpent { get; set; }
        [Required]
        public CryptoCurrency SpendingCurrency { get; set; }
        [Required]
        [Description("Amount received in satoshi or cents.")]
        public decimal AmountReceived { get; set; }
        [Required]
        public Currency ReceivingCurrency { get; set; }
        public int? ExchangeRate { get; set; }
        public override TransactionType TransactionType => TransactionType.Divestment;
        public override decimal TransactionWorth
        {
            get
            {
                return Math.Round(AmountReceived, 2);
            }
        }
    }
}
