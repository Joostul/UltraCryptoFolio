using System;
using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models
{
    public abstract class Transaction
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public int? Fee { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
