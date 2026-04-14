using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace DomainModel
{
    public class TransactionDto
    {
        private decimal _Value;
        private string _Category = string.Empty;

        [Key]
        public int TransactionId {  get; set; }
        public decimal Value { get { return _Value; } set { if (value < 0) { throw new Exception($"property {nameof(Value)} must be a positive number"); } _Value = value; } }
        public DateOnly Date { get; set;  }
        public string Category { get { return _Category; } set { if (string.IsNullOrWhiteSpace(value)) { throw new Exception($"property {nameof(Category)} must contain a value"); } _Category = value; } }
        public bool Fixed { get; set; }
        public bool Depletion { get; set; }
    }

    public sealed class FixedTransactionDto : TransactionDto
    {
        private int _Duration;
        public FixedTransactionDto()
        {
            Fixed = true;
        }
        public int Duration { get { return _Duration; } set { if (value < 0) { throw new Exception($"property {nameof(Duration)} must be a positive number"); } _Duration = value; }  }
    }
}
