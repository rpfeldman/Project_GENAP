using Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel;
using System.Runtime.CompilerServices;

namespace DataServices
{
    public sealed class DataProjectionService(IStateStorage StateStorage)
    {
        private IStateStorage _StateStorage = StateStorage;

        public decimal Net { get { return Income() - Expenses(); } }
        public bool Deficit { get { return Net < 0; } }

        public List<TransactionDto> GetAll()
        {
            return _StateStorage.GetAll();
        }
        public List<TransactionDto> GetExpenses()
        {
            return _StateStorage.GetTransaction(t => t.Depletion == true);
        }
        public List<TransactionDto> GetExpenses(string category)
        {
            return _StateStorage.GetTransaction(t => t.Depletion == true && t.Category == category);
        }

        public List<TransactionDto> GetIncome()
        {
            return _StateStorage.GetTransaction(t => t.Depletion == false);
        }
        public List<TransactionDto> GetIncome(string category)
        {
            return _StateStorage.GetTransaction(t => t.Depletion == false && t.Category == category);
        }

        public decimal Expenses()
        {
            decimal expenses = 0m;

            foreach (var item in GetExpenses())
            {
                expenses += item.Value;
            }

            return expenses;
        }
        public decimal Expenses(string category)
        {
            decimal expenses = 0m;

            foreach (var item in GetExpenses(category))
            {
                expenses += item.Value;
            }

            return expenses;
        }

        public decimal Income()
        {
            decimal income = 0m;

            foreach (var item in GetIncome())
            {
                income += item.Value;
            }

            return income;
        }
        public decimal Income(string category)
        {
            decimal income = 0m;

            foreach (var item in GetIncome(category))
            {
                income += item.Value;
            }

            return income;
        }
    }
}
