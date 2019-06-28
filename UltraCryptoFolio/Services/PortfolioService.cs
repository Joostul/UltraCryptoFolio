using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Models.Enums;
using UltraCryptoFolio.Repositories;

namespace UltraCryptoFolio.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private Portfolio _portfolio;
        private readonly IPriceRepository _priceRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository, IPriceRepository priceRepository)
        {
            _portfolioRepository = portfolioRepository;
            _priceRepository = priceRepository;
        }

        public async Task AddTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            if(_portfolio == null)
            {
                await GetPortfolioFromDatabaseAsync();
            }
            _portfolio.Transactions.AddRange(transactions);
        }

        public async Task< IDictionary<Currency, decimal>> GetCurrenciesWorth(IEnumerable<Currency> currencies)
        {
            if(_portfolio == null)
            {
                await GetPortfolioFromDatabaseAsync();
            }

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

        public async Task SavePortfolio()
        {
            await _portfolioRepository.SavePortfolioAsync(_portfolio);
        }

        private async Task GetPortfolioFromDatabaseAsync()
        {
            _portfolio = await _portfolioRepository.GetPortfolioAsync();
        }
    }
}
