using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models
{
    public class Investment : Transaction
    {
        [Required]
        [Description("Amount spent in satoshi or cents.")]
        public decimal AmountSpent { get; set; }
        [Required]
        public Currency SpendingCurrency { get; set; }
        [Required]
        [Description("Amount received in satoshi or cents.")]
        public long AmountReceived { get; set; }
        [Required]
        public CryptoCurrency ReveicingCurrency { get; set; }
        [Required]
        public int ExchangeRate { get; set; }
        public override TransactionType TransactionType => TransactionType.Investment;
    }
}
