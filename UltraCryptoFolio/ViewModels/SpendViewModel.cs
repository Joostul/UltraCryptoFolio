using System;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.ViewModels
{
    public class SpendViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        [Required]
        public long AmountSpent { get; set; }
        [Required]
        public CryptoCurrency SpendingCurrency { get; set; }
        public int? Fee { get; set; }
    }
}
