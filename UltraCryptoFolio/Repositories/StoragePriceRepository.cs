using Microsoft.WindowsAzure.Storage;
using System;
using System.Threading.Tasks;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories
{
    public class StoragePriceRepository : IPriceRepository
    {
        private IAzureStorageAccountRepository _storageAccount;

        public StoragePriceRepository(IAzureStorageAccountRepository storageAccount)
        {
            _storageAccount = storageAccount;
        }

        public Task<decimal> GetCurrentPriceAsync(Currency currency)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetPriceAtDateAsync(Currency currency, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
