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

            DMS.RemoveFixedTransaction(8);

            for (int i = 1; i <= 12; i++) 
            {
                Console.WriteLine($"mes {i}: ");

                foreach (var item in DPS.GetAllByMonth(i, 2026, DataProjectionService.Order.OrderByDate))
                {
                    var depletion = item.Depletion ? $"[{item.TransactionId}] " + "Se gasto " : "Se gano ";
                    Console.WriteLine(depletion + $"{item.Value:N2}$ en {item.Category} el {item.Date}");
                }

                Console.WriteLine($"\nResumen del mes {i}:\n");
                Console.WriteLine($"Gastos: {DPS.ExpensesByMonth(i, 2026):N2}$\nIngresos: {DPS.IncomeByMonth(i, 2026):N2}$\nBalance final: {DPS.NetByMonth(i, 2026):N2}$\n");
            }

            Console.WriteLine("\nResumen general:\n");
            Console.WriteLine($"Gastos: {DPS.Expenses():N2}$\nIngresos: {DPS.Income():N2}$\nBalance final: {DPS.GlobalNet:N2}$\n");



            void Test_Registrations()
            {
                    // Fixed expenses (with duration)
                    DRS.RegistExpense(85000m, new DateOnly(2026, 1, 1), 12, "Rent");
                    DRS.RegistExpense(15000m, new DateOnly(2026, 1, 5), 12, "Internet");
                    DRS.RegistExpense(8000m, new DateOnly(2026, 1, 5), 12, "Streaming");
                    DRS.RegistExpense(45000m, new DateOnly(2026, 1, 10), 6, "Gym");
                    DRS.RegistExpense(12000m, new DateOnly(2026, 2, 1), 12, "Phone insurance");

                    // Fixed incomes (with duration)
                    DRS.RegistIncome(120000m, new DateOnly(2026, 1, 1), 12, "Welfare");
                    DRS.RegistIncome(35000m, new DateOnly(2026, 1, 1), 6, "Scholarship");

                    // Daily expenses - January
                    DRS.RegistExpense(3500m, new DateOnly(2026, 1, 2), "Food");
                    DRS.RegistExpense(1200m, new DateOnly(2026, 1, 3), "Transport");
                    DRS.RegistExpense(8500m, new DateOnly(2026, 1, 4), "Food");
                    DRS.RegistExpense(2800m, new DateOnly(2026, 1, 7), "Food");
                    DRS.RegistExpense(15000m, new DateOnly(2026, 1, 8), "Clothing");
                    DRS.RegistExpense(4200m, new DateOnly(2026, 1, 10), "Going out");
                    DRS.RegistExpense(6500m, new DateOnly(2026, 1, 12), "Food");
                    DRS.RegistExpense(900m, new DateOnly(2026, 1, 14), "Transport");
                    DRS.RegistExpense(2300m, new DateOnly(2026, 1, 16), "Food");
                    DRS.RegistExpense(18000m, new DateOnly(2026, 1, 18), "Going out");
                    DRS.RegistExpense(5400m, new DateOnly(2026, 1, 20), "Food");
                    DRS.RegistExpense(1500m, new DateOnly(2026, 1, 22), "Transport");
                    DRS.RegistExpense(7800m, new DateOnly(2026, 1, 25), "Food");
                    DRS.RegistExpense(3200m, new DateOnly(2026, 1, 28), "Pharmacy");
                    DRS.RegistExpense(9500m, new DateOnly(2026, 1, 30), "Going out");

                    // Daily expenses - February
                    DRS.RegistExpense(4100m, new DateOnly(2026, 2, 2), "Food");
                    DRS.RegistExpense(1800m, new DateOnly(2026, 2, 4), "Transport");
                    DRS.RegistExpense(6700m, new DateOnly(2026, 2, 6), "Food");
                    DRS.RegistExpense(22000m, new DateOnly(2026, 2, 8), "Clothing");
                    DRS.RegistExpense(3900m, new DateOnly(2026, 2, 10), "Food");
                    DRS.RegistExpense(5600m, new DateOnly(2026, 2, 13), "Going out");
                    DRS.RegistExpense(2400m, new DateOnly(2026, 2, 15), "Food");
                    DRS.RegistExpense(1100m, new DateOnly(2026, 2, 17), "Transport");
                    DRS.RegistExpense(8200m, new DateOnly(2026, 2, 19), "Food");
                    DRS.RegistExpense(14000m, new DateOnly(2026, 2, 22), "Going out");
                    DRS.RegistExpense(3300m, new DateOnly(2026, 2, 24), "Food");
                    DRS.RegistExpense(2700m, new DateOnly(2026, 2, 26), "Pharmacy");
                    DRS.RegistExpense(5100m, new DateOnly(2026, 2, 28), "Food");

                    // Daily expenses - March
                    DRS.RegistExpense(3800m, new DateOnly(2026, 3, 2), "Food");
                    DRS.RegistExpense(1400m, new DateOnly(2026, 3, 4), "Transport");
                    DRS.RegistExpense(7300m, new DateOnly(2026, 3, 6), "Food");
                    DRS.RegistExpense(11000m, new DateOnly(2026, 3, 8), "Going out");
                    DRS.RegistExpense(4500m, new DateOnly(2026, 3, 10), "Food");
                    DRS.RegistExpense(950m, new DateOnly(2026, 3, 12), "Transport");
                    DRS.RegistExpense(6100m, new DateOnly(2026, 3, 14), "Food");
                    DRS.RegistExpense(35000m, new DateOnly(2026, 3, 16), "Clothing");
                    DRS.RegistExpense(2600m, new DateOnly(2026, 3, 18), "Food");
                    DRS.RegistExpense(8900m, new DateOnly(2026, 3, 20), "Going out");
                    DRS.RegistExpense(4800m, new DateOnly(2026, 3, 23), "Food");
                    DRS.RegistExpense(1700m, new DateOnly(2026, 3, 25), "Transport");
                    DRS.RegistExpense(5500m, new DateOnly(2026, 3, 27), "Food");
                    DRS.RegistExpense(4200m, new DateOnly(2026, 3, 30), "Pharmacy");

                    // Daily expenses - April
                    DRS.RegistExpense(3600m, new DateOnly(2026, 4, 1), "Food");
                    DRS.RegistExpense(1300m, new DateOnly(2026, 4, 2), "Transport");
                    DRS.RegistExpense(7500m, new DateOnly(2026, 4, 3), "Food");
                    DRS.RegistExpense(16000m, new DateOnly(2026, 4, 5), "Going out");
                    DRS.RegistExpense(2900m, new DateOnly(2026, 4, 6), "Food");
                    DRS.RegistExpense(5200m, new DateOnly(2026, 4, 8), "Food");

                    // Monthly salary
                    DRS.RegistIncome(850000m, new DateOnly(2026, 1, 1), "Salary");
                    DRS.RegistIncome(850000m, new DateOnly(2026, 2, 1), "Salary");
                    DRS.RegistIncome(850000m, new DateOnly(2026, 3, 1), "Salary");
                    DRS.RegistIncome(850000m, new DateOnly(2026, 4, 1), "Salary");

                    // Trading income
                    DRS.RegistIncome(125000m, new DateOnly(2026, 1, 8), "Trading");
                    DRS.RegistIncome(78000m, new DateOnly(2026, 1, 15), "Trading");
                    DRS.RegistIncome(210000m, new DateOnly(2026, 1, 22), "Trading");
                    DRS.RegistIncome(95000m, new DateOnly(2026, 2, 5), "Trading");
                    DRS.RegistIncome(340000m, new DateOnly(2026, 2, 12), "Trading");
                    DRS.RegistIncome(62000m, new DateOnly(2026, 2, 20), "Trading");
                    DRS.RegistIncome(185000m, new DateOnly(2026, 3, 3), "Trading");
                    DRS.RegistIncome(150000m, new DateOnly(2026, 3, 11), "Trading");
                    DRS.RegistIncome(88000m, new DateOnly(2026, 3, 19), "Trading");
                    DRS.RegistIncome(270000m, new DateOnly(2026, 3, 28), "Trading");
                    DRS.RegistIncome(115000m, new DateOnly(2026, 4, 4), "Trading");
                    DRS.RegistIncome(198000m, new DateOnly(2026, 4, 7), "Trading");

                    // Trading losses (as expenses)
                    DRS.RegistExpense(45000m, new DateOnly(2026, 1, 11), "Trading");
                    DRS.RegistExpense(82000m, new DateOnly(2026, 1, 19), "Trading");
                    DRS.RegistExpense(33000m, new DateOnly(2026, 2, 9), "Trading");
                    DRS.RegistExpense(120000m, new DateOnly(2026, 2, 18), "Trading");
                    DRS.RegistExpense(55000m, new DateOnly(2026, 3, 7), "Trading");
                    DRS.RegistExpense(28000m, new DateOnly(2026, 3, 15), "Trading");
                    DRS.RegistExpense(91000m, new DateOnly(2026, 3, 24), "Trading");
                    DRS.RegistExpense(42000m, new DateOnly(2026, 4, 2), "Trading");
                    DRS.RegistExpense(67000m, new DateOnly(2026, 4, 6), "Trading");

                    // Non-recurring income
                    DRS.RegistIncome(150000m, new DateOnly(2026, 1, 20), "GPU sale");
                    DRS.RegistIncome(45000m, new DateOnly(2026, 3, 5), "Side job");
                    DRS.RegistIncome(80000m, new DateOnly(2026, 4, 3), "Monitor sale");
            }
            
        }
    }
        
}
