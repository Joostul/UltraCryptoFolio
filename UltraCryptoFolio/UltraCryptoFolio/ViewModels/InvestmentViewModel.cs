using System;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.ViewModels
{
    public class InvestmentViewModel
    {
        [Required]
        public decimal AmountSpent { get; set; }
        [Required]
        public Currency SpendingCurrency { get; set; }
        [Required]
        public long AmountReceived { get; set; }
        [Required]
        public CryptoCurrency Receivingurrency { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public int? Fee { get; set; }
        public int? ExchangeRate { get; set; }
    }
}
