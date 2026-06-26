using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DataServices
{
    public class DataManagementService(IStateStorage<TransactionDto> StateStorage)
    {
        private IStateStorage<TransactionDto> _StateStorage = StateStorage;
        public async Task<OperationResult> UpdateTransactionAsync(int TransactionId, decimal? value = null, DateOnly? date = null, string? category = null, bool? depletion = null)
        {
            if (value < 1m || value > 1000000000m)
            {
                return OperationResult.FaultedOperation($"{nameof(value)} must be in the range of 1 to 1,000,000,000");
            }
            if (category is not null && category.IsWhiteSpace())
            {
                return OperationResult.FaultedOperation($"{nameof(category)} must have a content");
            }

            var GetEntityOperation = await _StateStorage.GetEntityAsync(TransactionId);

            if (!GetEntityOperation.HasValue)
            {
                return OperationResult.FaultedOperation($"Unable to find a transaction with the following id: {TransactionId}");
            } 
            TransactionDto Transaction = GetEntityOperation.Value!;

            Transaction.Category = category ?? Transaction.Category;
            Transaction.Value = value ?? Transaction.Value;
            Transaction.Date = date ?? Transaction.Date;
            Transaction.Depletion = depletion ?? Transaction.Depletion;

            return await _StateStorage.UpdateAsync(Transaction);
        }

        public async Task<OperationResult> RenameCategoryAsync() // TO - DO
       {
            throw new NotImplementedException();
       }

        public async Task<OperationResult> RemoveTransactionAsync(int TransactionId)
        {
            return await _StateStorage.DeleteAsync(TransactionId);
        }

        public async Task<OperationResult> RemoveFixedTransactionAsync(int CollectionId, int FromDuration)
        {
            var DeleteFromRangeOperation = await _StateStorage.DeleteFromRangeAsync(t => t is FixedTransactionDto && (t as FixedTransactionDto)!.FixedTransactionId == CollectionId && (t as FixedTransactionDto)!.Duration <= FromDuration);

            if (DeleteFromRangeOperation.Success)
            {
                if (DeleteFromRangeOperation.Result < 1) { return OperationResult.FaultedOperation($"Delete failed: no transactions matched in fixed transaction collection (CollectionId: {CollectionId}, FromDuration: {FromDuration}) — 0 rows affected"); };

                return OperationResult.SuccessfulOperation();
            }

            return OperationResult.FaultedOperation(DeleteFromRangeOperation.ErrorMessage);
        }
        public async Task<OperationResult> RemoveFixedTransactionAsync(int CollectionId)
        {
            var DeleteFromRangeOperation = await _StateStorage.DeleteFromRangeAsync(t => t is FixedTransactionDto && (t as FixedTransactionDto)!.FixedTransactionId == CollectionId);

            if (DeleteFromRangeOperation.Success)
            {
                if (DeleteFromRangeOperation.Result < 1) { return OperationResult.FaultedOperation($"Delete failed: no fixed transaction collection matched CollectionId {CollectionId} (0 rows affected)"); }
                ;

                return OperationResult.SuccessfulOperation();
            }

            return OperationResult.FaultedOperation(DeleteFromRangeOperation.ErrorMessage);
        }

        public async Task<OperationResult> RemoveFromCategoryAsync(string category)
        {
            var DeleteFromRangeOperation = await _StateStorage.DeleteFromRangeAsync(t => t.Category == category);

            if (DeleteFromRangeOperation.Success)
            {
                if (DeleteFromRangeOperation.Result < 1) { return OperationResult.FaultedOperation($"Delete failed: no transaction collection matched category '{category}' (0 rows affected)"); };

                return OperationResult.SuccessfulOperation();
            }

            return OperationResult.FaultedOperation(DeleteFromRangeOperation.ErrorMessage);
        }
        public async Task<OperationResult> RestartDataAsync()
        {
            return await _StateStorage.ClearStorageAsync();
        }
    }
}
