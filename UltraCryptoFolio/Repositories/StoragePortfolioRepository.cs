using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Repositories
{
    public class StoragePortfolioRepository : IPortfolioRepository
    {
        private CloudStorageAccount _storageAccount { get; set; }

        public StoragePortfolioRepository(CloudStorageAccount storageAccount)
        {
            _storageAccount = storageAccount;
        }

        public void AddTransaction(Transaction transaction, PortfolioUser user)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("portfolio");
            var blob = container.GetBlockBlobReference(user.UserEmail);
            //container.CreateIfNotExistsAsync
        }

        public Transaction GetTransaction(Guid Id, PortfolioUser user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions(PortfolioUser user)
        {
            throw new NotImplementedException();
        }
    }
}
