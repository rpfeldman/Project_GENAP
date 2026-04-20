using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Repositories
{
    public interface IStateStorage
    {
        public void Save(decimal value, DateOnly date, string category, bool depletion, bool isfixed, int? duration);
        public void Delete(int TransactionId);
        public void DeleteFromRange(Expression<Func<TransactionDto, bool>> predicate);
        public void Update(int TransactionId, decimal? value, DateOnly? date, string? category, bool? depletion);
        public void ClearStorage();
        public TransactionDto? GetTransaction(int TransactionId);
        public List<TransactionDto> GetTransactions(Expression<Func<TransactionDto, bool>> predicate);
        public List<TransactionDto> GetAll();
    }
}
