using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraCryptoFolio.Models;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public class StoragePortfolioRepository : IPortfolioRepository
    {

        public StoragePortfolioRepository()
        {

        }

        public void AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Currency, decimal> GetCurrenciesWorth()
        {
            throw new NotImplementedException();
        }

        public decimal GetCurrencyWorth(Currency currency)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalWorth()
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransaction(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
