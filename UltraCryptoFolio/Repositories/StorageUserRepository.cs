using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Repositories.DataAccessObjects;

namespace UltraCryptoFolio.Repositories
{
    public class StorageUserRepository : IUserRepository
    {
        private CloudStorageAccount _storageAccount;

        public StorageUserRepository(CloudStorageAccount storageAccount)
        {
            _storageAccount = storageAccount;
        }

        public async Task<Uri> AddUser(PortfolioUser user)
        {
            var userContainer = await GetPortfolioContainer();
            var userDao = user.ToDao();
            userDao.Id = Guid.NewGuid();
            var userBlob = userContainer.GetBlockBlobReference(user.UserEmail);
            var stringContent = JsonConvert.SerializeObject(userDao);
            await userBlob.UploadTextAsync(stringContent);
            return userBlob.Uri;
        }

        public async Task<PortfolioUser> GetUser(string userName)
        {
            var userContainer = await GetPortfolioContainer();
            var userBlob = userContainer.GetBlockBlobReference(userName);
            if (await userBlob.ExistsAsync())
            {
                var stringContent = await userBlob.DownloadTextAsync();
                var dao = JsonConvert.DeserializeObject<PortfolioUserDao>(stringContent);
                return dao.ToDomainModel();

            } else
            {
                throw new KeyNotFoundException();
            }
        }

        public Task RemoveUser(PortfolioUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUsername(PortfolioUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUserPassword(PortfolioUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserNameExists(string userName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CloudBlobContainer> GetPortfolioContainer()
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();
            var userContainer = blobClient.GetContainerReference("portfolios");
            await userContainer.CreateIfNotExistsAsync();
            return userContainer;
        }
    }
}
