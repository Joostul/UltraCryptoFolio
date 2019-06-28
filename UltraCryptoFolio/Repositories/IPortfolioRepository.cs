using System.Threading.Tasks;
using UltraCryptoFolio.Models.DomainModels;

namespace UltraCryptoFolio.Repositories
{
    public interface IPortfolioRepository
    {
        public Task<Portfolio> GetPortfolioAsync();
        public Task SavePortfolioAsync(Portfolio portfolio);
    }
}
