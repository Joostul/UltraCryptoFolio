using System;
using System.Collections.Generic;

namespace UltraCryptoFolio.Repositories.DataAccessObjects
{
    public class PortfolioDao
    {
        public Guid Id { get; set; }
        public IEnumerable<TransactionDao> Transactions { get; set; }
    }
}
