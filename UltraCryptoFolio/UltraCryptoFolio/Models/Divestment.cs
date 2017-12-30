﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Models
{
    public class Divestment : Transaction
    {
        [Required]
        [Description("Amount spent in satoshi or cents.")]
        public long AmountSpent { get; set; }
        [Required]
        public CryptoCurrency SpendingCurrency { get; set; }
        [Required]
        [Description("Amount received in satoshi or cents.")]
        public decimal AmountReceived { get; set; }
        [Required]
        public Currency Receivingurrency { get; set; }
        [Required]
        public int ExchangeRate { get; set; }
        public override TransactionType TransactionType => TransactionType.Divestment;
    }
}
