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
                _StateStorage.Update(TransactionId, value, date, category, depletion, duration);
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

        public int RemoveExpenses()
        {
            try
            {
                foreach (var item in _StateStorage.GetTransaction(t => t.Depletion == true))
                {
                    RemoveTransaction(item.TransactionId);
                }
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
                foreach (var item in _StateStorage.GetTransaction(t => t.Depletion == false))
                {
                    RemoveTransaction(item.TransactionId);
                }
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
                foreach (var item in _StateStorage.GetTransaction(t => t.Category == category))
                {
                    RemoveTransaction(item.TransactionId);
                }
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
