using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IStateStorage
    {
        public void Save(int value, DateOnly date, string category, bool depletion, bool isfixed);
        public void Remove(int TransactionId);
        public void Update(int TransactionId, int? NewValue, string? NewCategory, bool? NewDepletion, bool? NewFixed);
        public void ClearStorage();
        public TransactionDto? GetTransaction(int TransactionId);
        public List<TransactionDto> GetTransaction(Func<TransactionDto, bool> predicate);
        public List<TransactionDto> GetAll();
    }
}
