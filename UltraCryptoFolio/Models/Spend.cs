using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models
{
    public class Spend : Transaction
    {
        [Required]
        [Description("Amount spent in satoshi or cents.")]
        public long AmountSpent { get; set; }
        [Required]
        public CryptoCurrency SpendingCurrency { get; set; }
        public override TransactionType TransactionType => TransactionType.Spend;
    }
}
