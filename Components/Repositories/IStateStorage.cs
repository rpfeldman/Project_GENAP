using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IStateStorage
    {
        public Task<bool> SaveAsync(TransactionDto transaction);
        public Task<bool> DeleteAsync(int TransactionId);
        public Task<bool> DeleteFromRangeAsync(Expression<Func<TransactionDto, bool>> predicate);
        public Task<bool> UpdateAsync(int TransactionId, TransactionDto NewTransaction);
        public Task<bool> ClearStorage();
        public Task<TransactionDto?> GetTransactionAsync(int TransactionId);
        public Task<List<TransactionDto>> GetTransactions(Expression<Func<TransactionDto, bool>> predicate);
        public Task<List<TransactionDto>> GetAllAsync();
    }
}
