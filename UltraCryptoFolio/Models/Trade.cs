using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    }
}
