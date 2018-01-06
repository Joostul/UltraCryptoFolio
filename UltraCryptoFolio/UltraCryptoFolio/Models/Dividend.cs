using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    }
}
