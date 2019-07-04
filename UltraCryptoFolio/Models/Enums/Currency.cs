using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models.Enums
{
    public enum Currency
    {
        [Display(Name = "Unknown")]
        Unknown,
        [Display(Name = "Euro")]
        Euro,
        [Display(Name = "US Dollar")]
        Dollar,
        [Display(Name = "Bitcoin (Core)")]
        Bitcoin,
        [Display(Name = "Bitcoin Cash")]
        BitcoinCash,
        [Display(Name = "Bitcoin Gold")]
        BitcoinGold,
        [Display(Name = "Ethereum")]
        Ethereum,
        [Display(Name = "Ripple")]
        Ripple,
        [Display(Name = "Monero")]
        Monero,
        [Display(Name = "IOTA")]
        IOTA,
        [Display(Name = "NEO")]
        NEO,
        [Display(Name = "Stellar")]
        Stellar,
        [Display(Name = "Nano")]
        Nano,
        [Display(Name = "BicoinSV")]
        BicoinSV
    }
}
