using System;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public decimal AmountSpent { get; set; }
        public decimal Fee { get; set; }
        public Currency SpentCurrency { get; set; }
        public decimal SpentCurrencyPrice { get; set; }
        public decimal AmountReceived { get; set; }
        public Currency ReceivedCurrency { get; set; }
        public decimal ReceivedCurrencyPrice { get; set; }
    }
}