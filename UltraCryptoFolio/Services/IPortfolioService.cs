using System.Collections.Generic;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Services
{
    public interface IPortfolioService
    {
        decimal GetTotalWorth();
        Task<IDictionary<Currency, decimal>> GetCurrenciesWorth(IEnumerable<Currency> currencies);
        decimal GetCurrencyWorth(Currency currency);
        Task AddTransactionsAsync(IEnumerable<Transaction> transactions);
        Task SavePortfolio();
    }
}
