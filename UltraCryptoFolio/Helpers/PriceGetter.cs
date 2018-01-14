using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UltraCryptoFolio.Models;

namespace UltraCryptoFolio.Helpers
{
    public class PriceGetter : IPriceGetter
    {
        private static HttpClient client;
        private DateTime? _dateTime;

        public PriceGetter()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public PriceGetter(DateTime dateTime) : base()
        {
            _dateTime = dateTime;
        }

        public async Task<decimal> GetEuroPriceOfAsync(CryptoCurrency cryptoCurrency)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if(_dateTime == null)
            {

                switch (cryptoCurrency)
                {
                    case CryptoCurrency.BitcoinCash:
                        response = await client.GetAsync("price?fsym=BCH&tsyms=EUR");
                        break;
                    case CryptoCurrency.BitcoinGold:
                        response = await client.GetAsync("price?fsym=BTG&tsyms=EUR");
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
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content.ReadAsStringAsync().Result;
                    var data = JObject.Parse(jsonData);
                    var dataValue = data.Value<decimal>("EUR");
                    return dataValue;
                }
            }
            else
            {
                return GetEuroPriceOnDateAsync(cryptoCurrency, _dateTime ?? DateTime.Now).Result;
            }

            return 0;
        }

        public async Task<decimal> GetEuroPriceOnDateAsync(CryptoCurrency cryptoCurrency, DateTime dateTime)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var unixTimeStamp = (Int32)(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            switch (cryptoCurrency)
            {
                case CryptoCurrency.Bitcoin:
                    response = await client.GetAsync("dayAvg?fsym=BTC&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.BitcoinCash:
                    response = await client.GetAsync("dayAvg?fsym=BCH&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.BitcoinGold:
                    response = await client.GetAsync("dayAvg?fsym=BTG&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.Ethereum:
                    response = await client.GetAsync("dayAvg?fsym=ETHtsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.Ripple:
                    response = await client.GetAsync("dayAvg?fsym=XRP&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.Monero:
                    response = await client.GetAsync("dayAvg?fsym=XMR&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.IOTA:
                    response = await client.GetAsync("dayAvg?fsym=IOT&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.NEO:
                    response = await client.GetAsync("dayAvg?fsym=NEO&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.Stellar:
                    response = await client.GetAsync("dayAvg?fsym=XLM&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case CryptoCurrency.RaiBlocks:
                    response = await client.GetAsync("dayAvg?fsym=XRB&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                default:
                    return 0;
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JObject.Parse(jsonData);
                var dataValue = data.Value<decimal>("EUR");
                return dataValue;
            }

            return 0;
        }


        public void Dispose()
        {
            client.Dispose();
        }
    }
}
