using System;
using System.Collections.Generic;
using System.Linq;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Models.DomainModels
{
    public class Portfolio
    {
        public List<Transaction> Transactions { get; set; }
        public IEnumerable<Holding> Holdings {
            get
            {
                var holdings = new List<Holding>();

                foreach (var item in Enum.GetValues(typeof(Currency)).Cast<Currency>())
                {
                    var receivedTransactions = Transactions.Where(t => t.ReceivedCurrency == item).Select(t => t.AmountReceived).Sum();
                    var spentTransaction = Transactions.Where(t => t.SpentCurrency == item).Select(t => t.AmountSpent).Sum();
                    var totalAmountInCurrency = receivedTransactions - spentTransaction;
                    if(totalAmountInCurrency != 0)
                    {
                        holdings.Add(new Holding
                        {
                            Amount = totalAmountInCurrency,
                            Currency = item
                        });
                    }
                }

                return holdings;
            }
        }
        public Currency Currency { get; set; }
    }
}
