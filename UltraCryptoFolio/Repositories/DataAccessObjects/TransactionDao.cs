using System;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories.DataAccessObjects
{
    public class TransactionDao
    {
        public Guid Id { get; set; }
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
