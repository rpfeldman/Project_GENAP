using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModel
{
    public sealed class TransactionDto
    {
        [Key]
        public int TransactionId {  get; set; }
        public decimal Value { get; set; }
        public DateOnly Date { get; set;  }
        public Category Category { get; set; }
        public bool Fixed { get; set; }
        public bool Depletion { get; set; }
    }

    public enum Category
    {
        Clothes, Social, Girlfriend, Food, PropFirm, Trades
    }
}
