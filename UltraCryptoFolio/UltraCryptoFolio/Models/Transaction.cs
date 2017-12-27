using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Models
{
    public class Transaction
    {
        [Description("Amount received in satoshi or cents.")]
        public long AmountReceived { get; set; }
        [Description("Amount spent in satoshi or cents.")]
        public long AmountSpent { get; set; }
        public DateTime DateTime { get; set; }
        public Currency ReveicingCurrency { get;set; }
        public Currency SpendingCurrency { get; set; }
        public int Fee { get; set; }
        public int ExchangeRate { get; set; }
    }
}
