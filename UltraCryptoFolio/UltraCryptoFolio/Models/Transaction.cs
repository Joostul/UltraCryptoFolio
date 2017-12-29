using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Models
{
    public class Transaction
    {
        [Required]
        [Description("Amount received in satoshi or cents.")]
        public long AmountReceived { get; set; }
        [Required]
        [Description("Amount spent in satoshi or cents.")]
        public long AmountSpent { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        public Currency ReveicingCurrency { get;set; }
        [Required]
        public Currency SpendingCurrency { get; set; }
        public int? Fee { get; set; }
        public int? ExchangeRate { get; set; }
    }
}
