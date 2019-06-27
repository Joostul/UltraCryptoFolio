using System.Collections.Generic;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Services
{
    public interface IPortfolioService
    {
        public decimal GetTotalWorth();
        public IDictionary<Currency, decimal> GetCurrenciesWorth();
        public decimal GetCurrencyWorth(Currency currency);
        public Task SavePortfolio();
    }
}
