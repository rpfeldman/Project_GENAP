using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IStateStorage
    {
        public void Save(int value, DateOnly date, Category category, bool depletion, bool isfixed);
        public void Remove(int TransactionId);
        public void Update(int TransactionId, int? NewValue, Category? NewCategory, bool? NewDepletion, bool? NewFixed);
        public void ClearStorage();
        public TransactionDto? GetTransaction(int TransactionId);
        public List<TransactionDto> GetTransaction(Func<TransactionDto, bool> predicate);
        public List<TransactionDto> GetAll();
    }
}
