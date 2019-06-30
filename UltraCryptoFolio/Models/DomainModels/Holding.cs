using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Models.DomainModels
{
    public class Holding
    {
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public Currency PriceCurrency { get; set; }
    }
}
