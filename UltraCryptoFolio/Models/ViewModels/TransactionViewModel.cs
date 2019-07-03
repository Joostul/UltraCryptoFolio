using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Models.ViewModels
{
    public class TransactionViewModel : IValidatableObject
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public decimal AmountSpent { get; set; }
        public decimal Fee { get; set; }
        [Required]
        public Currency SpentCurrency { get; set; }
        public decimal SpentCurrencyPrice { get; set; }
        public decimal AmountReceived { get; set; }
        [Required]
        public Currency ReceivedCurrency { get; set; }
        public decimal ReceivedCurrencyPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(SpentCurrency == Currency.Unknown)
            {
                yield return new ValidationResult(
                    $"Currency cannot be {Currency.Unknown}.", 
                    new[] { $"{nameof(SpentCurrency)}" });
            } 
            if(ReceivedCurrency == Currency.Unknown)
            {
                yield return new ValidationResult(
                    $"Currency cannot be {Currency.Unknown}.",
                    new[] { $"{nameof(ReceivedCurrency)}" });
            }
        }
    }
}
