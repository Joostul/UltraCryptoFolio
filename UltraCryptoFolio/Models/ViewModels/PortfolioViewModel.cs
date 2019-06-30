namespace UltraCryptoFolio.Models.ViewModels
{
    public class PortfolioViewModel
    {
        public decimal TotalWorth { get; set; }
        public decimal TotalInvested { get; set; }
        public decimal TotalProfitLoss { get { return TotalWorth - TotalInvested; } }
    }
}
