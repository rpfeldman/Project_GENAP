using Repositories;
using DomainModel;
using System.IO;
using Microsoft.VisualBasic;
using DataServices;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ConsoleTest 
{
    internal class Program // TEMP
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var path = "test.db";
            var repo = new EF_SQLite_StateStorage(path, [14, 2]);
            var DPS = new DataProjectionService(repo);
            var DRS = new DataRegistrationService(repo);
            var DMS = new DataManagementService(repo);
            var today = DateOnly.FromDateTime(DateTime.Today);

            // Expenses
            DRS.RegistExpense(1200m, new DateOnly(2026, 1, 3), "Food");
            DRS.RegistExpense(350m, new DateOnly(2026, 1, 5), "Transport");
            DRS.RegistExpense(8500m, new DateOnly(2026, 1, 7), 12, "Rent");
            DRS.RegistExpense(2200m, new DateOnly(2026, 1, 8), "Food");
            DRS.RegistExpense(500m, new DateOnly(2026, 1, 10), "Entertainment");
            DRS.RegistExpense(1500m, new DateOnly(2026, 1, 12), "Clothing");
            DRS.RegistExpense(900m, new DateOnly(2026, 1, 14), "Transport");
            DRS.RegistExpense(3100m, new DateOnly(2026, 1, 15), "Food");
            DRS.RegistExpense(15000m, new DateOnly(2026, 1, 17), "Education");
            DRS.RegistExpense(750m, new DateOnly(2026, 1, 19), "Entertainment");
            DRS.RegistExpense(1800m, new DateOnly(2026, 1, 21), "Food");
            DRS.RegistExpense(400m, new DateOnly(2026, 1, 23), "Transport");
            DRS.RegistExpense(2500m, new DateOnly(2026, 1, 25), 6, "Utilities");
            DRS.RegistExpense(6200m, new DateOnly(2026, 1, 27), "Food");
            DRS.RegistExpense(1100m, new DateOnly(2026, 1, 29), "Entertainment");

            DRS.RegistExpense(950m, new DateOnly(2026, 2, 1), "Transport");
            DRS.RegistExpense(3400m, new DateOnly(2026, 2, 3), "Food");
            DRS.RegistExpense(1200m, new DateOnly(2026, 2, 5), "Clothing");
            DRS.RegistExpense(800m, new DateOnly(2026, 2, 7), "Entertainment");
            DRS.RegistExpense(2700m, new DateOnly(2026, 2, 9), "Food");
            DRS.RegistExpense(5000m, new DateOnly(2026, 2, 11), 12, "Insurance");
            DRS.RegistExpense(650m, new DateOnly(2026, 2, 13), "Transport");
            DRS.RegistExpense(1900m, new DateOnly(2026, 2, 15), "Food");
            DRS.RegistExpense(3300m, new DateOnly(2026, 2, 17), "Education");
            DRS.RegistExpense(450m, new DateOnly(2026, 2, 19), "Entertainment");
            DRS.RegistExpense(1400m, new DateOnly(2026, 2, 21), "Food");
            DRS.RegistExpense(700m, new DateOnly(2026, 2, 23), "Transport");
            DRS.RegistExpense(2100m, new DateOnly(2026, 2, 25), "Clothing");
            DRS.RegistExpense(4800m, new DateOnly(2026, 2, 27), "Food");

            DRS.RegistExpense(1300m, new DateOnly(2026, 3, 1), "Transport");
            DRS.RegistExpense(2800m, new DateOnly(2026, 3, 3), "Food");
            DRS.RegistExpense(600m, new DateOnly(2026, 3, 5), "Entertainment");
            DRS.RegistExpense(9500m, new DateOnly(2026, 3, 7), 12, "Rent");
            DRS.RegistExpense(1700m, new DateOnly(2026, 3, 9), "Food");
            DRS.RegistExpense(550m, new DateOnly(2026, 3, 11), "Transport");
            DRS.RegistExpense(3600m, new DateOnly(2026, 3, 13), "Food");
            DRS.RegistExpense(1000m, new DateOnly(2026, 3, 15), "Entertainment");
            DRS.RegistExpense(2300m, new DateOnly(2026, 3, 17), 6, "Utilities");
            DRS.RegistExpense(850m, new DateOnly(2026, 3, 19), "Transport");
            DRS.RegistExpense(4100m, new DateOnly(2026, 3, 21), "Food");
            DRS.RegistExpense(1600m, new DateOnly(2026, 3, 23), "Clothing");
            DRS.RegistExpense(7200m, new DateOnly(2026, 3, 25), "Education");
            DRS.RegistExpense(500m, new DateOnly(2026, 3, 27), "Entertainment");
            DRS.RegistExpense(2900m, new DateOnly(2026, 3, 29), "Food");

            DRS.RegistExpense(1100m, new DateOnly(2026, 4, 1), "Transport");
            DRS.RegistExpense(3500m, new DateOnly(2026, 4, 2), "Food");
            DRS.RegistExpense(800m, new DateOnly(2026, 4, 3), "Entertainment");
            DRS.RegistExpense(2000m, new DateOnly(2026, 4, 4), "Clothing");
            DRS.RegistExpense(1500m, new DateOnly(2026, 4, 5), "Food");
            DRS.RegistExpense(4500m, new DateOnly(2026, 4, 6), "Education");
            DRS.RegistExpense(650m, new DateOnly(2026, 4, 7), "Transport");
            DRS.RegistExpense(2600m, new DateOnly(2026, 4, 8), "Food");

            // Incomes
            DRS.RegistIncome(250000m, new DateOnly(2026, 1, 1), "Salary");
            DRS.RegistIncome(45000m, new DateOnly(2026, 1, 10), "Trading");
            DRS.RegistIncome(12000m, new DateOnly(2026, 1, 15), "Trading");
            DRS.RegistIncome(8500m, new DateOnly(2026, 1, 20), "Freelance");
            DRS.RegistIncome(32000m, new DateOnly(2026, 1, 25), "Trading");
            DRS.RegistIncome(250000m, new DateOnly(2026, 2, 1), "Salary");
            DRS.RegistIncome(18000m, new DateOnly(2026, 2, 8), "Trading");
            DRS.RegistIncome(55000m, new DateOnly(2026, 2, 14), "Trading");
            DRS.RegistIncome(7500m, new DateOnly(2026, 2, 18), "Freelance");
            DRS.RegistIncome(41000m, new DateOnly(2026, 2, 22), "Trading");
            DRS.RegistIncome(15000m, new DateOnly(2026, 2, 28), "Sale");
            DRS.RegistIncome(250000m, new DateOnly(2026, 3, 1), "Salary");
            DRS.RegistIncome(22000m, new DateOnly(2026, 3, 6), "Trading");
            DRS.RegistIncome(63000m, new DateOnly(2026, 3, 12), "Trading");
            DRS.RegistIncome(9800m, new DateOnly(2026, 3, 16), "Freelance");
            DRS.RegistIncome(37000m, new DateOnly(2026, 3, 20), "Trading");
            DRS.RegistIncome(28000m, new DateOnly(2026, 3, 26), "Trading");
            DRS.RegistIncome(250000m, new DateOnly(2026, 4, 1), "Salary");
            DRS.RegistIncome(19000m, new DateOnly(2026, 4, 3), "Trading");
            DRS.RegistIncome(48000m, new DateOnly(2026, 4, 5), "Trading");
            DRS.RegistIncome(11000m, new DateOnly(2026, 4, 7), "Freelance");
            DRS.RegistIncome(35000m, new DateOnly(2026, 4, 8), "Trading");

            // Trading losses (as expenses)
            DRS.RegistExpense(15000m, new DateOnly(2026, 1, 12), "Trading");
            DRS.RegistExpense(8000m, new DateOnly(2026, 1, 22), "Trading");
            DRS.RegistExpense(22000m, new DateOnly(2026, 2, 6), "Trading");
            DRS.RegistExpense(11000m, new DateOnly(2026, 2, 20), "Trading");
            DRS.RegistExpense(18500m, new DateOnly(2026, 3, 4), "Trading");
            DRS.RegistExpense(9000m, new DateOnly(2026, 3, 18), "Trading");
            DRS.RegistExpense(14000m, new DateOnly(2026, 3, 28), "Trading");
            DRS.RegistExpense(7500m, new DateOnly(2026, 4, 4), "Trading");
            DRS.RegistExpense(19000m, new DateOnly(2026, 4, 8), "Trading");
        }
    }
}
