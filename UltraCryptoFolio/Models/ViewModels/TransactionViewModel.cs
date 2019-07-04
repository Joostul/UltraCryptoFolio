using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("Amount spent")]
        public decimal AmountSpent { get; set; }
        [Display(Name = "Fee")]
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
                    $"{nameof(SpentCurrency)} cannot be {SpentCurrency}.", 
                    new[] { $"{nameof(SpentCurrency)}" });
            } 
            if(ReceivedCurrency == Currency.Unknown)
            {
                yield return new ValidationResult(
                    $"{nameof(ReceivedCurrency)} cannot be {SpentCurrency}.",
                    new[] { $"{nameof(ReceivedCurrency)}" });
            }
            if(DateTime == DateTime.MinValue)
            {
                yield return new ValidationResult(
                    $"{nameof(DateTime)} must be filled.",
                    new[] { $"{nameof(ReceivedCurrency)}" });
            }
        }
    }
}
