using System;
using System.Collections.Generic;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Repositories
{
    public interface IPortfolioRepository
    {
        public IEnumerable<Transaction> GetTransactions(PortfolioUser user);
        public Transaction GetTransaction(Guid Id, PortfolioUser user);
        void AddTransaction(Transaction transaction, PortfolioUser user);
    }
}
