using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio
{
    public interface IPriceGetter : IDisposable
    {
        Task<decimal> GetEuroPriceOfAsync(CryptoCurrency cryptoCurrency);
        Task<decimal> GetEuroPriceOnDateAsync(CryptoCurrency cryptoCurrency, DateTime dateTime);
    }
}
