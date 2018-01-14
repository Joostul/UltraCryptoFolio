using System;
using System.Threading.Tasks;
using UltraCryptoFolio;
using UltraCryptoFolio.Models;

namespace UnitTests.Mock
{
    public class MockPriceGetter : IPriceGetter
    {
        public void Dispose()
        {
            
        }

        public Task<decimal> GetEuroPriceOfAsync(CryptoCurrency cryptoCurrency, DateTime? dateTime = null)
        {
            switch (cryptoCurrency)
            {
                case CryptoCurrency.Bitcoin:
                    return Task.FromResult(Constants.BitcoinPrice);
                case CryptoCurrency.BitcoinCash:
                    return Task.FromResult(Constants.BitcoinCashPrice);
                case CryptoCurrency.BitcoinGold:
                    return Task.FromResult(Constants.BitcoinGoldPrice);
                case CryptoCurrency.Ethereum:
                    return Task.FromResult(Constants.EthereumPrice);
                case CryptoCurrency.Ripple:
                    return Task.FromResult(Constants.RipplePrice);
                case CryptoCurrency.Monero:
                    return Task.FromResult(Constants.MoneroPrice);
                case CryptoCurrency.IOTA:
                    return Task.FromResult(Constants.IOTAPrice);
                case CryptoCurrency.NEO:
                    return Task.FromResult(Constants.NEOPrice);
                case CryptoCurrency.Stellar:
                    return Task.FromResult(Constants.StellarPrice);
                case CryptoCurrency.RaiBlocks:
                    return Task.FromResult(Constants.RaiBlocksPrice);
                default:
                    return Task.FromResult(0m);
            }
        }
    }
}
