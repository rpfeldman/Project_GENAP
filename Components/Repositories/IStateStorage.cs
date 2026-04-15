using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IStateStorage
    {
        public void Save(decimal value, DateOnly date, string category, bool depletion, bool isfixed, int? duration);
        public void Delete(int TransactionId);
        public void Update(int TransactionId, decimal? value, DateOnly? date, string? category, bool? depletion, int? duration);
        public void DeleteFromRange(Func<TransactionDto, bool> predicate);
        public void ClearStorage();
        public TransactionDto? GetTransaction(int TransactionId);
        public List<TransactionDto> GetTransaction(Func<TransactionDto, bool> predicate);
        public List<TransactionDto> GetAll();
    }
}
