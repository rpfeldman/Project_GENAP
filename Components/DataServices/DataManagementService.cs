using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DataServices
{
    public class DataManagementService(IStateStorage StateStorage)
    {
        private IStateStorage _StateStorage = StateStorage;

        public int UpdateTransaction(int TransactionId, decimal? value = null, DateOnly? date = null, string? category = null, bool? depletion = null, int? duration = null)
        {
            try
            {
                _StateStorage.Update(TransactionId, value, date, category, depletion);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int RemoveTransaction(int TransactionId)
        {
            try
            {
                _StateStorage.Delete(TransactionId);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int RemoveFixedTransaction(int CollectionId, int FromMonth)
        {
            try
            {
                _StateStorage.DeleteFromRange(t => t is FixedTransactionDto && (t as FixedTransactionDto)!.FixedTransactionId == CollectionId && t.Date.Month > FromMonth);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public int RemoveFixedTransaction(int CollectionId)
        {
            try
            {
                _StateStorage.DeleteFromRange(t => t is FixedTransactionDto && (t as FixedTransactionDto)!.FixedTransactionId == CollectionId);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int RemoveExpenses()
        {
            try
            {
                _StateStorage.DeleteFromRange(t => t.Depletion == true);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int RemoveIncome()
        {
            try
            {
                _StateStorage.DeleteFromRange(t => t.Depletion == false);
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int RemoveFromCategory(string category)
        {
            try
            {
                _StateStorage.DeleteFromRange(t => t.Category == category);
                return 0;
            }
            catch (Exception)
            { 
                return 1;
            }
        }

        public int RestartData()
        {
            try
            {
                _StateStorage.ClearStorage();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}
