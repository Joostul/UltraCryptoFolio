using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UltraCryptoFolio.Extensions;

namespace UltraCryptoFolio.Models
{
    [JsonConverter(typeof(TransactionConverter))]
    public abstract class Transaction
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }
        public int? Fee { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        [Description("Amount that the sending value is worth at the time of the transaction.")]
        private decimal _transactionWorth;
        public decimal TransactionWorth
        {
            get { return _transactionWorth; }
            set { _transactionWorth = value; }
        }
    }
}
