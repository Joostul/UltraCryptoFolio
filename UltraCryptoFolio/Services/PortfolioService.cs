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

        public void AddTransactions(IEnumerable<Transaction> transactions)
        {
            _portfolio.Transactions.AddRange(transactions);
        }

        public async Task<IDictionary<Currency, decimal>> GetCurrenciesWorth(IEnumerable<Currency> currencies)
        {
            await TrySetPortfolio();

            return new Dictionary<Currency, decimal>();
        }

        public async Task<decimal> GetCurrencyWorth(Currency currency)
        {
            await TrySetPortfolio();
            throw new NotImplementedException();
        }

        public async Task<decimal> GetTotalWorth()
        {
            await TrySetPortfolio();

            decimal totalWorth = 0;

            foreach (var holding in _portfolio.Holdings)
            {                
                if(holding.Currency == Currency.Euro || holding.Currency == Currency.Dollar)
                {}
                else
                {
                    totalWorth += holding.Amount * await _priceRepository.GetCurrentPriceAsync(holding.Currency, holding.PriceCurrency);
                }
            }

            return totalWorth;
        }
        public async Task<decimal> GetTotalInvested()
        {
            decimal totalInvested = 0;

            foreach (var holding in _portfolio.Holdings)
            {
                if (holding.Currency == Currency.Euro || holding.Currency == Currency.Dollar)
                {
                    totalInvested += holding.Amount * await _priceRepository.GetCurrentPriceAsync(holding.Currency, holding.PriceCurrency);
                }
            }

            return -totalInvested;
        }

        public async Task SavePortfolio()
        {
            await _portfolioRepository.SavePortfolioAsync(_portfolio);
        }

        private async Task TrySetPortfolio()
        {
            if(_portfolio == null)
            {
                _portfolio = await _portfolioRepository.GetPortfolioAsync();
            }
        }

        public void CreateExamplePortfolio()
        {
            _portfolio = new Portfolio
            {
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        AmountReceived = 1,
                        AmountSpent = 250,
                        DateTime = new DateTime(2019, 1, 1),
                        Fee = 0,
                        ReceivedCurrency = Currency.BitcoinCash,
                        SpentCurrency = Currency.Euro,
                        ReceivedCurrencyPrice = 250,
                        SpentCurrencyPrice = 1
                    }
                }
            };
        }
    }
}
