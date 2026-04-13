using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Repositories
{
    public sealed class EF_SQLite_StateStorage : IStateStorage
    {
        private StateStorageDbContext Context;
        public EF_SQLite_StateStorage(string StorageFilePath, int[] DecimalValuePrecision)
        {
            var options = new DbContextOptionsBuilder().UseSqlite($"Data source={StorageFilePath}").Options;
            Context = new(options, DecimalValuePrecision); // As SQLite save decimal data type as TEXT, 'DecimalValuePrecision' it's irrelevant in this case
            Context.Database.EnsureCreated();
        }
        public void ClearStorage()
        {
           Context.TransactionsTable.RemoveRange(Context.TransactionsTable);
           Context.SaveChanges();
        }

        public List<TransactionDto> GetAll()
        {
            return Context.TransactionsTable.ToList();
        }

        public TransactionDto? GetTransaction(int TransactionId)
        {
            return Context.TransactionsTable.Where(t => t.TransactionId == TransactionId).FirstOrDefault();
        }

        public List<TransactionDto> GetTransaction(Func<TransactionDto, bool> predicate)
        { 
            return Context.TransactionsTable.Where(predicate).ToList();
        }

        public void Remove(int TransactionId)
        {
            var Transaction = Context.TransactionsTable.Where(t => t.TransactionId == TransactionId).FirstOrDefault() ?? throw new Exception("Unexistent transaction");

            Context.TransactionsTable.Remove(Transaction);
            Context.SaveChanges();
        }

        public void Save(decimal value, DateOnly date, string category, bool depletion, bool isfixed = false, int? duration = null)
        {
            if(isfixed && duration == null)
            {
                throw new ArgumentException($"{nameof(duration)} must contain a value if {nameof(isfixed)} is true");
            }

            value = value < 0 ? throw new Exception($"{nameof(value)} must be a positive number") : value;

            category = string.IsNullOrWhiteSpace(category) ? throw new ArgumentException($"{nameof(category)} must contain a value") : category;

            var Transaction = isfixed ? new FixedTransactionDto{ Value = value, Date = date, Category = category, Depletion = depletion, Fixed = isfixed, Duration = (int)duration! } : new TransactionDto() { Value = value, Date = date, Category = category, Depletion = depletion, Fixed = isfixed };

            Context.Add(Transaction);
            Context.SaveChanges();
        }

        public void Update(int TransactionId, decimal? NewValue = null, DateOnly? NewDate = null, string? NewCategory = null, bool? NewDepletion = null, bool? NewFixed = null, int? NewDuration = null)
        {
            var Transaction = Context.TransactionsTable.Where(t => t.TransactionId == TransactionId).FirstOrDefault() ?? throw new Exception("Unexistent transaction");

            Transaction.Value = (decimal)(NewValue == null || NewValue < 0 ? Transaction.Value : NewValue);
            Transaction.Category = string.IsNullOrWhiteSpace(NewCategory) ? Transaction.Category : NewCategory;
            Transaction.Depletion = NewDepletion ?? Transaction.Depletion;
            Transaction.Fixed = NewFixed ?? Transaction.Fixed;
            Transaction.Date = NewDate ?? Transaction.Date;

            Context.TransactionsTable.Update(Transaction);
            Context.SaveChanges();
        }
    }
}
