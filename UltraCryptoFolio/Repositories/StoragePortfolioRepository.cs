using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UltraCryptoFolio.Extensions;
using UltraCryptoFolio.Models.DomainModels;
using UltraCryptoFolio.Repositories.DataAccessObjects;

namespace UltraCryptoFolio.Repositories
{
    public class StoragePortfolioRepository : IPortfolioRepository
    {
        private readonly IAzureStorageAccountRepository _storageAccount;
        private string _userName;
        
        public StoragePortfolioRepository(IAzureStorageAccountRepository storageAccount, IHttpContextAccessor httpContextAccessor)
        {
            _storageAccount = storageAccount;

            if (httpContextAccessor.HttpContext.User != null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                _userName = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            }
        }

        public async Task<bool> HasPortfolioAsync()
        {
            return await _storageAccount.BlobExistsAsync("portfolios", _userName);
        }

        public async Task<Portfolio> GetPortfolioAsync()
        {
            if (await _storageAccount.BlobExistsAsync("portfolios", _userName))
            {
                var stringContent = await _storageAccount.DownloadTextAsync("portfolios", _userName);
                var dao = JsonConvert.DeserializeObject<PortfolioDao>(stringContent);
                dao.Transactions = dao.Transactions.OrderByDescending(t => t.DateTime);
                return dao.ToDomainModel();
            } else
            {
                return null;
            }
        }

        public async Task SavePortfolioAsync(Portfolio portfolio)
        {
            if (!await _storageAccount.BlobExistsAsync("portfolios", _userName))
            {
                var stringContent = JsonConvert.SerializeObject(portfolio);
                await _storageAccount.UploadTextAsync(stringContent, "portfolios", _userName);
            }
            else
            {
                var currentPortfolio = await GetPortfolioAsync();
                var newTransactions = portfolio.Transactions.Where(t => !currentPortfolio.Transactions.Any(e => e.Id == t.Id));
                currentPortfolio.Transactions.AddRange(newTransactions);
                var dao = currentPortfolio.ToDao();
                var stringContent = JsonConvert.SerializeObject(dao);
                await _storageAccount.UploadTextAsync(stringContent, "portfolios", _userName);
            }
        }
    }
}
