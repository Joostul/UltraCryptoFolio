using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Repositories.DataAccessObjects;

namespace UltraCryptoFolio.Repositories
{
    public class StorageUserRepository : IUserRepository
    {
        private readonly IAzureStorageAccountRepository _storageAccount;
        private string _userName { get; set; }

        public StorageUserRepository(IAzureStorageAccountRepository storageAccount, IHttpContextAccessor httpContextAccessor)
        {
            _storageAccount = storageAccount;

            if (httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                _userName = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            }
        }

        public async Task AddUserAsync(PortfolioUser user)
        {
            var userDao = user.ToDao();
            userDao.Id = Guid.NewGuid();
            var stringContent = JsonConvert.SerializeObject(userDao);
            await _storageAccount.UploadTextAsync(stringContent, "users", user.UserEmail);
        }

        public async Task<PortfolioUser> GetUserAsync()
        {
            return await GetUserAsync(_userName);
        }

        public async Task<PortfolioUser> GetUserAsync(string userName)
        {
            if (await _storageAccount.BlobExistsAsync("users", userName))
            {
                var stringContent = await _storageAccount.DownloadTextAsync("users", userName);
                var dao = JsonConvert.DeserializeObject<PortfolioUserDao>(stringContent);
                return dao.ToDomainModel();

            }
            else
            {
                return null;
            }
        }

        public Task RemoveUserAsync(PortfolioUser user)
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
    }
}
