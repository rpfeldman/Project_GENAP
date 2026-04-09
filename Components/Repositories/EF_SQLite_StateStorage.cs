using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Repositories
{
    public sealed class EF_SQLite_StateStorage : IStateStorage
    {
        private StateStorageDbContext Context;
        public EF_SQLite_StateStorage(string StorageFilePath)
        {
            var options = new DbContextOptionsBuilder().UseSqlite($"Data source={StorageFilePath}").LogTo(Console.WriteLine).Options;
            Context = new(options);
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

        public void Save(int value, DateOnly date, Category category, bool depletion, bool isfixed = false)
        {
            var Transaction = new TransactionDto() { Value = value, Date = date, Category = category, Depletion = depletion, Fixed = isfixed };

            Context.Add(Transaction);
            Context.SaveChanges();
        }

        public void Update(int TransactionId, int? NewValue = null, Category? NewCategory = null, bool? NewDepletion = null, bool? NewFixed = null)
        {
            var Transaction = Context.TransactionsTable.Where(t => t.TransactionId == TransactionId).FirstOrDefault() ?? throw new Exception("Unexistent transaction");
            Transaction.Value = NewValue ?? Transaction.Value;
            Transaction.Category = NewCategory ?? Transaction.Category;
            Transaction.Depletion = NewDepletion ?? Transaction.Depletion;
            Transaction.Fixed = NewFixed ?? Transaction.Fixed;

            Context.TransactionsTable.Update(Transaction);
        }
    }

    internal class StateStorageDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<TransactionDto> TransactionsTable { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransactionDto>().HasKey(p => p.TransactionId);
            modelBuilder.Entity<TransactionDto>().Property(p => p.Category).HasConversion<string>();
            modelBuilder.Entity<TransactionDto>().Property(p => p.Value).HasPrecision(14, 2);
        }
    }
}
