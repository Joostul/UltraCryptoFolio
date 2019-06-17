using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public class ApiPriceRepository : IPriceRepository
    {
        private HttpClient _httpClient;

        public ApiPriceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<decimal> GetCurrentPriceAsync(Currency currency)
        {
            return await GetPriceAtDateAsync(currency, DateTime.UtcNow);
        }

        public async Task<decimal> GetPriceAtDateAsync(Currency currency, DateTime date)
        {
            var unixTimeStamp = date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            HttpResponseMessage response = new HttpResponseMessage();

            switch (currency)
            {
                case Currency.Bitcoin:
                    response = await _httpClient.GetAsync("dayAvg?fsym=BTC&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.BitcoinCash:
                    response = await _httpClient.GetAsync("price?fsym=BCH&tsyms=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.BitcoinGold:
                    response = await _httpClient.GetAsync("dayAvg?fsym=BTG&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.Ethereum:
                    response = await _httpClient.GetAsync("dayAvg?fsym=ETHtsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.Ripple:
                    response = await _httpClient.GetAsync("dayAvg?fsym=XRP&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.Monero:
                    response = await _httpClient.GetAsync("dayAvg?fsym=XMR&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.IOTA:
                    response = await _httpClient.GetAsync("dayAvg?fsym=IOT&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.NEO:
                    response = await _httpClient.GetAsync("dayAvg?fsym=NEO&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.Stellar:
                    response = await _httpClient.GetAsync("dayAvg?fsym=XLM&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.Nano:
                    response = await _httpClient.GetAsync("dayAvg?fsym=XRB&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.BicoinSV:
                    response = await _httpClient.GetAsync("dayAvg?fsym=BSV&tsym=EUR&toTs=" + unixTimeStamp);
                    break;
                case Currency.Euro:
                case Currency.Dollar:
                default:
                    throw new InvalidOperationException(); // Not so clean but sure
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JObject.Parse(jsonData);
                var dataValue = data.Value<decimal>("EUR");
                return dataValue;
            } else
            {
                throw new InvalidOperationException(); // Not so clean but sure
            }
        }
    }
}
