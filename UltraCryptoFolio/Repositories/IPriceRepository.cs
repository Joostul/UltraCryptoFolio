using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public interface IPriceRepository
    {
        public Task<decimal> GetCurrentPriceAsync(Currency currency);
        public Task<decimal> GetPriceAtDateAsync(Currency currency, DateTime date);
    }
}
