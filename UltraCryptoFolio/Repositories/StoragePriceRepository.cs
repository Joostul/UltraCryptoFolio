using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public class StoragePriceRepository : IPriceRepository
    {
        private IApiPriceRepository _apiPriceRepository;
        private IAzureStorageAccountRepository _storageAccount;

        public StoragePriceRepository(IApiPriceRepository apiPriceRepository, IAzureStorageAccountRepository storageAccount)
        {
            _storageAccount = storageAccount;
            _apiPriceRepository = apiPriceRepository;
        }

        public Task<decimal> GetCurrentPriceAsync(Currency currency, Currency priceCurrency)
        {
            return _apiPriceRepository.GetCurrentPriceAsync(currency, priceCurrency);
        }

        public Task<decimal> GetPriceAtDateAsync(Currency currency, Currency priceCurrency, DateTime date)
        {
            return _apiPriceRepository.GetPriceAtDateAsync(currency, priceCurrency, date);
        }
    }
}
