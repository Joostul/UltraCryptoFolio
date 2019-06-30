using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public interface IApiPriceRepository
    {
        public Task<decimal> GetCurrentPriceAsync(Currency currency, Currency priceCurrency);
        public Task<decimal> GetPriceAtDateAsync(Currency currency, Currency priceCurrency, DateTime date);
    }
}
