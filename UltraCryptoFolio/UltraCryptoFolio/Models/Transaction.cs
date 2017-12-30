using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models
{
    public abstract class Transaction
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        public int? Fee { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
