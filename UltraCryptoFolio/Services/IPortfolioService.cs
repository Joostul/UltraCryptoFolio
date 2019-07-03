using System.Collections.Generic;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Services
{
    public interface IPortfolioService
    {
        Task<decimal> GetTotalWorth();
        Task<decimal> GetTotalInvested();
        Task<IDictionary<Currency, decimal>> GetCurrenciesWorth(IEnumerable<Currency> currencies);
        Task<decimal> GetCurrencyWorth(Currency currency);
        Task AddTransactions(IEnumerable<Transaction> transactions);
        Task<IEnumerable<Transaction>> GetTransactions();
        Task SavePortfolio();
        void CreateExamplePortfolio();
    }
}
