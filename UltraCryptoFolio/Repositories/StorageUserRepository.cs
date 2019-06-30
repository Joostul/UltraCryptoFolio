using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Models.Enums;
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
            await _storageAccount.UploadTextAsync(stringContent, "tempusers", user.UserEmail);
        }

        public async Task<PortfolioUserDao> GetUserAsync()
        {
            return await GetUserAsync(_userName);
        }

        public async Task<PortfolioUserDao> GetUserAsync(string userName)
        {
            if (await _storageAccount.BlobExistsAsync("users", userName))
            {
                var stringContent = await _storageAccount.DownloadTextAsync("users", userName);
                return JsonConvert.DeserializeObject<PortfolioUserDao>(stringContent);
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateUserAsync(PortfolioUser user)
        {
            if(await _storageAccount.BlobExistsAsync("users", user.UserEmail))
            {
                var userDao = user.ToDao();
                var stringContent = JsonConvert.SerializeObject(userDao);
                await _storageAccount.UploadTextAsync(stringContent, "users", user.UserEmail);
            }
        }

        public Task DeleteUserAsync(PortfolioUser user)
        {
            throw new NotImplementedException();
        }

        public async Task AddTempUserAsync(PortfolioUser user)
        {
            var userDao = user.ToDao();
            userDao.Id = Guid.NewGuid();
            var stringContent = JsonConvert.SerializeObject(userDao);
            await _storageAccount.UploadTextAsync(stringContent, "tempusers", userDao.Id.ToString());
        }

        public async Task RegisterTempUserAsync(Guid userId)
        {
            var tempUser = await GetTempUserAsync(userId);
            if(tempUser != null)
            {
                await _storageAccount.MoveBlobAsync("tempusers", tempUser.Id.ToString(), "users", tempUser.UserName);
                var verifiedUser = tempUser.ToDomainModel();
                verifiedUser.State = UserState.Verified;
                await UpdateUserAsync(verifiedUser);
            }
        }

        public async Task<PortfolioUserDao> GetTempUserAsync(Guid userId)
        {
            if (await _storageAccount.BlobExistsAsync("tempusers", userId.ToString()))
            {
                var stringContent = await _storageAccount.DownloadTextAsync("tempusers", userId.ToString());
                return JsonConvert.DeserializeObject<PortfolioUserDao>(stringContent);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> TempUserExists(Guid userId)
        {
            if(await _storageAccount.BlobExistsAsync("tempusers", userId.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
