using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataServices
{
    public sealed class DataRegistrationService(IStateStorage StateStorage)
    {
        private IStateStorage _StateStorage = StateStorage;

        public int RegistExpense(int value, DateOnly date, string category = "Uncategorized")
        {
            try
            {
                _StateStorage.Save(value, date, category, true, false, null);
                return 0;
            }
            catch (Exception)
            {
                return 1;
                throw;
            }
        }
        public int RegistExpense(int value, DateOnly date, int duration, string category = "Uncategorized")
        {
            try
            {
                _StateStorage.Save(value, date, category, true, true, duration);
                return 0;
            }
            catch (Exception)
            {
                return 1;
                throw;
            }
        }

        public int RegistIncome(int value, DateOnly date, string category = "Uncategorized")
        {
            try
            {
                _StateStorage.Save(value, date, category, false, false, null);
                return 0; 
            }
            catch (Exception)
            {
                return 1;
                throw;
            }
        }
        public int RegistIncome(int value, DateOnly date, int duration, string category = "Uncategorized")
        {
            try
            {
                _StateStorage.Save(value, date, category, false, true, duration);
                return 0;
            }
            catch (Exception)
            {
                return 1;
                throw;
            }
        }
    }
}
