using System;
using System.Collections.Generic;
using UltraCryptoFolio.Models;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public interface IPortfolioRepository
    {
        public IEnumerable<Transaction> GetTransactions();
        public Transaction GetTransaction(Guid Id);
        public void AddTransaction(Transaction transaction);
        public decimal GetTotalWorth();
        public IDictionary<Currency, decimal> GetCurrenciesWorth();
        public decimal GetCurrencyWorth(Currency currency);
    }
}
