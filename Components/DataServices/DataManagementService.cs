using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices
{
    public class DataManagementService(IStateStorage StateStorage)
    {
        private IStateStorage _StateStorage = StateStorage;

        public int UpdateTransaction(int TransactionId, decimal? value = null, DateOnly? date = null, string? category = null, bool? depletion = null, int? duration = null)
        {
            try
            {
                _StateStorage.Update(TransactionId, value, date, category, depletion, null);
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
                _StateStorage.Remove(TransactionId);
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}
