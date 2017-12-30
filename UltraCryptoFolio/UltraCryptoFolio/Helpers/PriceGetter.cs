using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Helpers
{
    public class PriceGetter : IDisposable
    {
        private static HttpClient client;

        public PriceGetter()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.coinmarketcap.com/v1/ticker/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<double> GetEuroPriceOfAsync(Currency currency)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            switch (currency)
            {
                case Currency.Unknown:
                case Currency.Dollar:
                    return 0;
                case Currency.Euro:
                    return 1;
                case Currency.BitcoinCash:
                    response = await client.GetAsync("bitcoin-cash" + "/?convert=EUR");
                    break;
                case Currency.Bitcoin:
                case Currency.Etherium:
                case Currency.Ripple:
                case Currency.Monero:
                    response = await client.GetAsync(currency.ToString().ToLower() + "/?convert=EUR");
                    break;
                default:
                    break;
            }
            if(response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                dynamic data = JArray.Parse(jsonData);
                return data[0].price_eur;
            }

            return 0;
        }

        public async Task<double> GetDollarPriceOfAsync(Currency currency)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            switch (currency)
            {
                case Currency.Unknown:
                case Currency.Dollar:
                    return 0;
                case Currency.Euro:
                    return 1;
                case Currency.BitcoinCash:
                    response = await client.GetAsync("bitcoin-cash");
                    break;
                case Currency.Bitcoin:
                case Currency.Etherium:
                case Currency.Ripple:
                case Currency.Monero:
                    response = await client.GetAsync(currency.ToString().ToLower());
                    break;
                default:
                    break;
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                dynamic data = JArray.Parse(jsonData);
                return data[0].price_eur;
            }

            return 0;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
