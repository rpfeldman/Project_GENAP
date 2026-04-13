using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainModel
{
    public class TransactionDto
    {
        [Key]
        public int TransactionId {  get; set; }
        public decimal Value { get; set; }
        public DateOnly Date { get; set;  }
        public string Category { get; set; } = string.Empty;
        public bool Fixed { get; set; }
        public bool Depletion { get; set; }
    }

    public sealed class FixedTransactionDto : TransactionDto
    {
        public FixedTransactionDto()
        {
            Fixed = true;
        }
        public int Duration { get; set; }
    }
}
