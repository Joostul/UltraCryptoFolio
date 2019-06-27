using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;
using UltraCryptoFolio.Repositories;

namespace UltraCryptoFolio.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _repository;

        public PortfolioService(IPortfolioRepository repository)
        {
            _repository = repository;
        }

        public IDictionary<Currency, decimal> GetCurrenciesWorth()
        {
            //var transactions = _repository.GetTransactions();

            return new Dictionary<Currency, decimal>();
        }

        public decimal GetCurrencyWorth(Currency currency)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalWorth()
        {
            throw new NotImplementedException();
        }

        public Task SavePortfolio()
        {
            throw new NotImplementedException();
        }
    }
}
