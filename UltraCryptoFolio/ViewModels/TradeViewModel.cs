using System;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.ViewModels
{
    public class TradeViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        [Required]
        public long AmountSpent { get; set; }
        [Required]
        public CryptoCurrency SpendingCurrency { get; set; }
        [Required]
        public long AmountReceived { get; set; }
        [Required]
        public CryptoCurrency ReceivingCurrency { get; set; }
        public int? ExchangeRate { get; set; }
        public int? Fee { get; set; }
    }
}
