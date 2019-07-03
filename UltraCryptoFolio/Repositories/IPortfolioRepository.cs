using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Repositories
{
    public interface IPortfolioRepository
    {
        Task<bool> HasPortfolioAsync();
        public Task<Portfolio> GetPortfolioAsync();
        public Task SavePortfolioAsync(Portfolio portfolio);
    }
}
