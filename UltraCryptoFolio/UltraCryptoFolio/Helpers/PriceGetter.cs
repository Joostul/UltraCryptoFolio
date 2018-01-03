using Newtonsoft.Json.Linq;
using System;
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
            client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //client.BaseAddress = new Uri("https://api.coinmarketcap.com/v1/ticker/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<decimal> GetEuroPriceOfAsync(CryptoCurrency cryptoCurrency)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            switch (cryptoCurrency)
            {
                case CryptoCurrency.BitcoinCash:
                    response = await client.GetAsync("price?fsym=BCH&tsyms=EUR");
                    //response = await client.GetAsync("bitcoin-cash" + "/?convert=EUR");
                    break;
                case CryptoCurrency.BitcoinGold:
                    response = await client.GetAsync("price?fsym=BTG&tsyms=EUR");
                    //response = await client.GetAsync("bitcoin-gold" + "/?convert=EUR");
                    break;
                case CryptoCurrency.NEO:
                    response = await client.GetAsync("price?fsym=NEO&tsyms=EUR");
                    break;
                case CryptoCurrency.RaiBlocks:
                    response = await client.GetAsync("price?fsym=XRB&tsyms=EUR");
                    break;
                case CryptoCurrency.Stellar:
                    response = await client.GetAsync("price?fsym=XLM&tsyms=EUR");
                    break;
                case CryptoCurrency.IOTA:
                    response = await client.GetAsync("price?fsym=IOT&tsyms=EUR");
                    break;
                case CryptoCurrency.Bitcoin:
                    response = await client.GetAsync("price?fsym=BTC&tsyms=EUR");
                    break;
                case CryptoCurrency.Ethereum:
                    response = await client.GetAsync("price?fsym=ETH&tsyms=EUR");
                    break;
                case CryptoCurrency.Ripple:
                    response = await client.GetAsync("price?fsym=XRP&tsyms=EUR");
                    break;
                case CryptoCurrency.Monero:
                    response = await client.GetAsync("price?fsym=XMR&tsyms=EUR");
                    break;
                default:
                    break;
            }
            if(response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JObject.Parse(jsonData);
                var dataValue = data.Value<decimal>("EUR");
                return dataValue;
            }

            return 0;
        }

        //public async Task<double> GetDollarPriceOfAsync(Currency currency)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();

        //    switch (currency)
        //    {
        //        case Currency.Unknown:
        //        case Currency.Dollar:
        //            return 1;
        //        case Currency.Euro:
        //            return 0;
        //        case Currency.BitcoinCash:
        //            response = await client.GetAsync("bitcoin-cash");
        //            break;
        //        case Currency.Bitcoin:
        //        case Currency.Etherium:
        //        case Currency.Ripple:
        //        case Currency.Monero:
        //            response = await client.GetAsync(currency.ToString().ToLower());
        //            break;
        //        default:
        //            break;
        //    }
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonData = response.Content.ReadAsStringAsync().Result;
        //        dynamic data = JArray.Parse(jsonData);
        //        return data[0].price_usd;
        //    }

        //    return 0;
        //}

        public async Task<decimal> GetPriceOnDate(CryptoCurrency cryptoCurrency, DateTime dateTime, Currency currency)
        {


            return 0;
        }


        public void Dispose()
        {
            client.Dispose();
        }
    }
}
