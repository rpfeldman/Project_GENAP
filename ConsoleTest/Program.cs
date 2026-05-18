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
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var path = "test.db";
            var repo = new EF_SQLite_StateStorage(path, [14, 2]);
            var DPS = new DataProjectionService(repo);
            var DRS = new DataRegistrationService(repo);
            var DMS = new DataManagementService(repo);
            var today = DateOnly.FromDateTime(DateTime.Today);

            var Result = await DPS.GetGlobalResultAsync();

            Console.Write($"{Result:N2}$");

            async Task TestTransactions()
            {
                Console.WriteLine("Registrando...");
                // ===== GASTOS FIJOS LARGO PLAZO =====
                await DRS.RegistExpenseAsync(180000m, new DateOnly(2026, 1, 1), 60, "Alquiler");
                await DRS.RegistExpenseAsync(45000m, new DateOnly(2026, 1, 1), 60, "Expensas");
                await DRS.RegistExpenseAsync(28000m, new DateOnly(2026, 1, 5), 60, "Internet");
                await DRS.RegistExpenseAsync(15000m, new DateOnly(2026, 1, 5), 60, "Celular");
                await DRS.RegistExpenseAsync(8500m, new DateOnly(2026, 1, 10), 60, "Spotify");
                await DRS.RegistExpenseAsync(12000m, new DateOnly(2026, 1, 10), 60, "Netflix");
                await DRS.RegistExpenseAsync(22000m, new DateOnly(2026, 1, 20), 60, "Prepaga");
                await DRS.RegistExpenseAsync(18000m, new DateOnly(2026, 2, 1), 60, "Seguro auto");
                await DRS.RegistExpenseAsync(95000m, new DateOnly(2026, 3, 5), 36, "Cuota Auto");
                await DRS.RegistExpenseAsync(35000m, new DateOnly(2026, 1, 15), 12, "Gimnasio");
                await DRS.RegistExpenseAsync(40000m, new DateOnly(2027, 1, 15), 12, "Gimnasio");
                await DRS.RegistExpenseAsync(48000m, new DateOnly(2028, 1, 15), 12, "Gimnasio");

                // ===== INGRESOS FIJOS LARGO PLAZO =====
                await DRS.RegistIncomeAsync(850000m, new DateOnly(2026, 1, 5), 12, "Sueldo");
                await DRS.RegistIncomeAsync(1100000m, new DateOnly(2027, 1, 5), 12, "Sueldo");
                await DRS.RegistIncomeAsync(1400000m, new DateOnly(2028, 1, 5), 12, "Sueldo");
                await DRS.RegistIncomeAsync(1750000m, new DateOnly(2029, 1, 5), 12, "Sueldo");
                await DRS.RegistIncomeAsync(2100000m, new DateOnly(2030, 1, 5), 13, "Sueldo");
                await DRS.RegistIncomeAsync(120000m, new DateOnly(2026, 1, 1), 36, "Plan social");

                // ===== 2026 =====
                await DRS.RegistExpenseAsync(8500m, new DateOnly(2026, 1, 3), "Supermercado");
                await DRS.RegistExpenseAsync(12000m, new DateOnly(2026, 1, 6), "Salidas");
                await DRS.RegistExpenseAsync(15000m, new DateOnly(2026, 1, 11), "Ropa");
                await DRS.RegistExpenseAsync(6700m, new DateOnly(2026, 1, 13), "Supermercado");
                await DRS.RegistExpenseAsync(18000m, new DateOnly(2026, 1, 17), "Salidas");
                await DRS.RegistExpenseAsync(9200m, new DateOnly(2026, 1, 22), "Supermercado");
                await DRS.RegistExpenseAsync(7800m, new DateOnly(2026, 1, 27), "Comida");
                await DRS.RegistIncomeAsync(180000m, new DateOnly(2026, 1, 8), "Trading");
                await DRS.RegistIncomeAsync(95000m, new DateOnly(2026, 1, 16), "Trading");
                await DRS.RegistIncomeAsync(250000m, new DateOnly(2026, 1, 23), "Trading");
                await DRS.RegistExpenseAsync(85000m, new DateOnly(2026, 1, 10), "Trading");
                await DRS.RegistExpenseAsync(140000m, new DateOnly(2026, 1, 21), "Trading");

                await DRS.RegistExpenseAsync(9800m, new DateOnly(2026, 2, 2), "Supermercado");
                await DRS.RegistExpenseAsync(14000m, new DateOnly(2026, 2, 6), "Salidas");
                await DRS.RegistExpenseAsync(22000m, new DateOnly(2026, 2, 12), "Ropa");
                await DRS.RegistExpenseAsync(35000m, new DateOnly(2026, 2, 18), "Salidas");
                await DRS.RegistExpenseAsync(10500m, new DateOnly(2026, 2, 23), "Supermercado");
                await DRS.RegistIncomeAsync(320000m, new DateOnly(2026, 2, 5), "Trading");
                await DRS.RegistIncomeAsync(150000m, new DateOnly(2026, 2, 13), "Trading");
                await DRS.RegistIncomeAsync(80000m, new DateOnly(2026, 2, 28), "Venta usado");
                await DRS.RegistExpenseAsync(110000m, new DateOnly(2026, 2, 9), "Trading");

                await DRS.RegistExpenseAsync(11000m, new DateOnly(2026, 3, 2), "Supermercado");
                await DRS.RegistExpenseAsync(16000m, new DateOnly(2026, 3, 6), "Salidas");
                await DRS.RegistExpenseAsync(45000m, new DateOnly(2026, 3, 13), "Ropa");
                await DRS.RegistExpenseAsync(28000m, new DateOnly(2026, 3, 20), "Salidas");
                await DRS.RegistExpenseAsync(12000m, new DateOnly(2026, 3, 25), "Supermercado");
                await DRS.RegistIncomeAsync(190000m, new DateOnly(2026, 3, 7), "Trading");
                await DRS.RegistIncomeAsync(410000m, new DateOnly(2026, 3, 14), "Trading");
                await DRS.RegistIncomeAsync(45000m, new DateOnly(2026, 3, 26), "Devolución impuestos");
                await DRS.RegistExpenseAsync(180000m, new DateOnly(2026, 3, 24), "Trading");

                await DRS.RegistExpenseAsync(12500m, new DateOnly(2026, 4, 1), "Supermercado");
                await DRS.RegistExpenseAsync(18000m, new DateOnly(2026, 4, 5), "Salidas");
                await DRS.RegistExpenseAsync(38000m, new DateOnly(2026, 4, 11), "Ropa");
                await DRS.RegistExpenseAsync(42000m, new DateOnly(2026, 4, 19), "Salidas");
                await DRS.RegistExpenseAsync(13500m, new DateOnly(2026, 4, 24), "Supermercado");
                await DRS.RegistIncomeAsync(280000m, new DateOnly(2026, 4, 4), "Trading");
                await DRS.RegistIncomeAsync(165000m, new DateOnly(2026, 4, 12), "Trading");
                await DRS.RegistIncomeAsync(150000m, new DateOnly(2026, 4, 25), "Freelance");
                await DRS.RegistExpenseAsync(120000m, new DateOnly(2026, 4, 8), "Trading");

                await DRS.RegistExpenseAsync(13800m, new DateOnly(2026, 5, 2), "Supermercado");
                await DRS.RegistExpenseAsync(22000m, new DateOnly(2026, 5, 6), "Salidas");
                await DRS.RegistExpenseAsync(35000m, new DateOnly(2026, 5, 15), "Ropa");
                await DRS.RegistIncomeAsync(350000m, new DateOnly(2026, 5, 6), "Trading");
                await DRS.RegistIncomeAsync(180000m, new DateOnly(2026, 5, 10), "Trading");
                await DRS.RegistExpenseAsync(140000m, new DateOnly(2026, 5, 9), "Trading");

                await DRS.RegistExpenseAsync(15000m, new DateOnly(2026, 6, 3), "Supermercado");
                await DRS.RegistExpenseAsync(28000m, new DateOnly(2026, 6, 8), "Salidas");
                await DRS.RegistExpenseAsync(45000m, new DateOnly(2026, 6, 18), "Vacaciones");
                await DRS.RegistIncomeAsync(420000m, new DateOnly(2026, 6, 5), "Trading");
                await DRS.RegistIncomeAsync(125000m, new DateOnly(2026, 6, 20), "Trading");

                await DRS.RegistExpenseAsync(16500m, new DateOnly(2026, 7, 4), "Supermercado");
                await DRS.RegistExpenseAsync(85000m, new DateOnly(2026, 7, 10), "Vacaciones");
                await DRS.RegistExpenseAsync(32000m, new DateOnly(2026, 7, 22), "Salidas");
                await DRS.RegistIncomeAsync(280000m, new DateOnly(2026, 7, 8), "Trading");
                await DRS.RegistExpenseAsync(95000m, new DateOnly(2026, 7, 15), "Trading");

                await DRS.RegistExpenseAsync(18000m, new DateOnly(2026, 8, 5), "Supermercado");
                await DRS.RegistExpenseAsync(45000m, new DateOnly(2026, 8, 12), "Ropa");
                await DRS.RegistIncomeAsync(380000m, new DateOnly(2026, 8, 6), "Trading");
                await DRS.RegistIncomeAsync(220000m, new DateOnly(2026, 8, 19), "Trading");

                await DRS.RegistExpenseAsync(19500m, new DateOnly(2026, 9, 3), "Supermercado");
                await DRS.RegistExpenseAsync(38000m, new DateOnly(2026, 9, 14), "Salidas");
                await DRS.RegistIncomeAsync(290000m, new DateOnly(2026, 9, 7), "Trading");
                await DRS.RegistExpenseAsync(150000m, new DateOnly(2026, 9, 21), "Trading");

                await DRS.RegistExpenseAsync(21000m, new DateOnly(2026, 10, 4), "Supermercado");
                await DRS.RegistExpenseAsync(52000m, new DateOnly(2026, 10, 18), "Ropa");
                await DRS.RegistIncomeAsync(310000m, new DateOnly(2026, 10, 9), "Trading");
                await DRS.RegistIncomeAsync(180000m, new DateOnly(2026, 10, 25), "Trading");

                await DRS.RegistExpenseAsync(22500m, new DateOnly(2026, 11, 5), "Supermercado");
                await DRS.RegistExpenseAsync(45000m, new DateOnly(2026, 11, 14), "Salidas");
                await DRS.RegistIncomeAsync(420000m, new DateOnly(2026, 11, 8), "Trading");
                await DRS.RegistExpenseAsync(125000m, new DateOnly(2026, 11, 22), "Trading");

                await DRS.RegistExpenseAsync(28000m, new DateOnly(2026, 12, 6), "Supermercado");
                await DRS.RegistExpenseAsync(150000m, new DateOnly(2026, 12, 15), "Regalos");
                await DRS.RegistExpenseAsync(85000m, new DateOnly(2026, 12, 24), "Salidas");
                await DRS.RegistIncomeAsync(480000m, new DateOnly(2026, 12, 10), "Trading");
                await DRS.RegistIncomeAsync(250000m, new DateOnly(2026, 12, 28), "Aguinaldo");

                // ===== 2027 =====
                await DRS.RegistExpenseAsync(32000m, new DateOnly(2027, 1, 5), "Supermercado");
                await DRS.RegistExpenseAsync(55000m, new DateOnly(2027, 1, 12), "Salidas");
                await DRS.RegistIncomeAsync(380000m, new DateOnly(2027, 1, 9), "Trading");
                await DRS.RegistExpenseAsync(180000m, new DateOnly(2027, 1, 22), "Trading");

                await DRS.RegistExpenseAsync(35000m, new DateOnly(2027, 2, 7), "Supermercado");
                await DRS.RegistExpenseAsync(68000m, new DateOnly(2027, 2, 18), "Ropa");
                await DRS.RegistIncomeAsync(450000m, new DateOnly(2027, 2, 11), "Trading");
                await DRS.RegistIncomeAsync(220000m, new DateOnly(2027, 2, 25), "Trading");

                await DRS.RegistExpenseAsync(38000m, new DateOnly(2027, 3, 4), "Supermercado");
                await DRS.RegistExpenseAsync(85000m, new DateOnly(2027, 3, 15), "Salidas");
                await DRS.RegistIncomeAsync(520000m, new DateOnly(2027, 3, 8), "Trading");
                await DRS.RegistExpenseAsync(220000m, new DateOnly(2027, 3, 19), "Trading");

                await DRS.RegistExpenseAsync(42000m, new DateOnly(2027, 4, 6), "Supermercado");
                await DRS.RegistExpenseAsync(95000m, new DateOnly(2027, 4, 17), "Ropa");
                await DRS.RegistIncomeAsync(380000m, new DateOnly(2027, 4, 10), "Trading");
                await DRS.RegistIncomeAsync(180000m, new DateOnly(2027, 4, 24), "Freelance");

                await DRS.RegistExpenseAsync(45000m, new DateOnly(2027, 5, 8), "Supermercado");
                await DRS.RegistExpenseAsync(120000m, new DateOnly(2027, 5, 19), "Salidas");
                await DRS.RegistIncomeAsync(620000m, new DateOnly(2027, 5, 12), "Trading");

                await DRS.RegistExpenseAsync(48000m, new DateOnly(2027, 6, 3), "Supermercado");
                await DRS.RegistExpenseAsync(180000m, new DateOnly(2027, 6, 14), "Vacaciones");
                await DRS.RegistIncomeAsync(420000m, new DateOnly(2027, 6, 9), "Trading");
                await DRS.RegistExpenseAsync(280000m, new DateOnly(2027, 6, 25), "Trading");

                await DRS.RegistExpenseAsync(52000m, new DateOnly(2027, 7, 5), "Supermercado");
                await DRS.RegistExpenseAsync(350000m, new DateOnly(2027, 7, 12), "Vacaciones");
                await DRS.RegistIncomeAsync(580000m, new DateOnly(2027, 7, 15), "Trading");

                await DRS.RegistExpenseAsync(55000m, new DateOnly(2027, 8, 4), "Supermercado");
                await DRS.RegistExpenseAsync(125000m, new DateOnly(2027, 8, 16), "Ropa");
                await DRS.RegistIncomeAsync(490000m, new DateOnly(2027, 8, 11), "Trading");
                await DRS.RegistIncomeAsync(220000m, new DateOnly(2027, 8, 27), "Trading");

                await DRS.RegistExpenseAsync(58000m, new DateOnly(2027, 9, 6), "Supermercado");
                await DRS.RegistExpenseAsync(95000m, new DateOnly(2027, 9, 18), "Salidas");
                await DRS.RegistIncomeAsync(640000m, new DateOnly(2027, 9, 9), "Trading");
                await DRS.RegistExpenseAsync(320000m, new DateOnly(2027, 9, 23), "Trading");

                await DRS.RegistExpenseAsync(62000m, new DateOnly(2027, 10, 7), "Supermercado");
                await DRS.RegistExpenseAsync(180000m, new DateOnly(2027, 10, 19), "Ropa");
                await DRS.RegistIncomeAsync(520000m, new DateOnly(2027, 10, 12), "Trading");
                await DRS.RegistIncomeAsync(280000m, new DateOnly(2027, 10, 26), "Trading");

                await DRS.RegistExpenseAsync(68000m, new DateOnly(2027, 11, 5), "Supermercado");
                await DRS.RegistExpenseAsync(150000m, new DateOnly(2027, 11, 17), "Salidas");
                await DRS.RegistIncomeAsync(720000m, new DateOnly(2027, 11, 10), "Trading");

                await DRS.RegistExpenseAsync(85000m, new DateOnly(2027, 12, 8), "Supermercado");
                await DRS.RegistExpenseAsync(380000m, new DateOnly(2027, 12, 18), "Regalos");
                await DRS.RegistExpenseAsync(220000m, new DateOnly(2027, 12, 26), "Salidas");
                await DRS.RegistIncomeAsync(680000m, new DateOnly(2027, 12, 12), "Trading");
                await DRS.RegistIncomeAsync(550000m, new DateOnly(2027, 12, 28), "Aguinaldo");

                // ===== 2028 =====
                await DRS.RegistExpenseAsync(95000m, new DateOnly(2028, 1, 6), "Supermercado");
                await DRS.RegistExpenseAsync(180000m, new DateOnly(2028, 1, 18), "Salidas");
                await DRS.RegistIncomeAsync(620000m, new DateOnly(2028, 1, 10), "Trading");
                await DRS.RegistExpenseAsync(280000m, new DateOnly(2028, 1, 24), "Trading");

                await DRS.RegistExpenseAsync(105000m, new DateOnly(2028, 2, 8), "Supermercado");
                await DRS.RegistExpenseAsync(150000m, new DateOnly(2028, 2, 19), "Ropa");
                await DRS.RegistIncomeAsync(780000m, new DateOnly(2028, 2, 12), "Trading");
                await DRS.RegistIncomeAsync(320000m, new DateOnly(2028, 2, 26), "Trading");

                await DRS.RegistExpenseAsync(115000m, new DateOnly(2028, 3, 5), "Supermercado");
                await DRS.RegistExpenseAsync(220000m, new DateOnly(2028, 3, 17), "Salidas");
                await DRS.RegistIncomeAsync(880000m, new DateOnly(2028, 3, 9), "Trading");
                await DRS.RegistExpenseAsync(380000m, new DateOnly(2028, 3, 22), "Trading");

                await DRS.RegistExpenseAsync(125000m, new DateOnly(2028, 4, 7), "Supermercado");
                await DRS.RegistExpenseAsync(280000m, new DateOnly(2028, 4, 18), "Ropa");
                await DRS.RegistIncomeAsync(620000m, new DateOnly(2028, 4, 11), "Trading");
                await DRS.RegistIncomeAsync(380000m, new DateOnly(2028, 4, 25), "Freelance");

                await DRS.RegistExpenseAsync(135000m, new DateOnly(2028, 5, 9), "Supermercado");
                await DRS.RegistExpenseAsync(320000m, new DateOnly(2028, 5, 21), "Salidas");
                await DRS.RegistIncomeAsync(950000m, new DateOnly(2028, 5, 13), "Trading");

                await DRS.RegistExpenseAsync(145000m, new DateOnly(2028, 6, 4), "Supermercado");
                await DRS.RegistExpenseAsync(450000m, new DateOnly(2028, 6, 15), "Vacaciones");
                await DRS.RegistIncomeAsync(720000m, new DateOnly(2028, 6, 10), "Trading");
                await DRS.RegistExpenseAsync(420000m, new DateOnly(2028, 6, 26), "Trading");

                await DRS.RegistExpenseAsync(160000m, new DateOnly(2028, 7, 6), "Supermercado");
                await DRS.RegistExpenseAsync(680000m, new DateOnly(2028, 7, 14), "Vacaciones");
                await DRS.RegistIncomeAsync(1100000m, new DateOnly(2028, 7, 17), "Trading");

                await DRS.RegistExpenseAsync(175000m, new DateOnly(2028, 8, 5), "Supermercado");
                await DRS.RegistExpenseAsync(280000m, new DateOnly(2028, 8, 17), "Ropa");
                await DRS.RegistIncomeAsync(820000m, new DateOnly(2028, 8, 12), "Trading");
                await DRS.RegistIncomeAsync(450000m, new DateOnly(2028, 8, 28), "Trading");

                await DRS.RegistExpenseAsync(185000m, new DateOnly(2028, 9, 7), "Supermercado");
                await DRS.RegistExpenseAsync(220000m, new DateOnly(2028, 9, 19), "Salidas");
                await DRS.RegistIncomeAsync(980000m, new DateOnly(2028, 9, 10), "Trading");
                await DRS.RegistExpenseAsync(520000m, new DateOnly(2028, 9, 24), "Trading");

                await DRS.RegistExpenseAsync(195000m, new DateOnly(2028, 10, 8), "Supermercado");
                await DRS.RegistExpenseAsync(380000m, new DateOnly(2028, 10, 20), "Ropa");
                await DRS.RegistIncomeAsync(850000m, new DateOnly(2028, 10, 13), "Trading");
                await DRS.RegistIncomeAsync(420000m, new DateOnly(2028, 10, 27), "Trading");

                await DRS.RegistExpenseAsync(220000m, new DateOnly(2028, 11, 6), "Supermercado");
                await DRS.RegistExpenseAsync(320000m, new DateOnly(2028, 11, 18), "Salidas");
                await DRS.RegistIncomeAsync(1200000m, new DateOnly(2028, 11, 11), "Trading");

                await DRS.RegistExpenseAsync(280000m, new DateOnly(2028, 12, 9), "Supermercado");
                await DRS.RegistExpenseAsync(680000m, new DateOnly(2028, 12, 19), "Regalos");
                await DRS.RegistExpenseAsync(450000m, new DateOnly(2028, 12, 27), "Salidas");
                await DRS.RegistIncomeAsync(1100000m, new DateOnly(2028, 12, 13), "Trading");
                await DRS.RegistIncomeAsync(800000m, new DateOnly(2028, 12, 28), "Aguinaldo");

                // ===== 2029 =====
                await DRS.RegistExpenseAsync(320000m, new DateOnly(2029, 1, 7), "Supermercado");
                await DRS.RegistExpenseAsync(450000m, new DateOnly(2029, 1, 19), "Salidas");
                await DRS.RegistIncomeAsync(1400000m, new DateOnly(2029, 1, 11), "Trading");
                await DRS.RegistExpenseAsync(580000m, new DateOnly(2029, 1, 25), "Trading");

                await DRS.RegistExpenseAsync(350000m, new DateOnly(2029, 2, 9), "Supermercado");
                await DRS.RegistExpenseAsync(380000m, new DateOnly(2029, 2, 20), "Ropa");
                await DRS.RegistIncomeAsync(1650000m, new DateOnly(2029, 2, 13), "Trading");
                await DRS.RegistIncomeAsync(620000m, new DateOnly(2029, 2, 27), "Trading");

                await DRS.RegistExpenseAsync(380000m, new DateOnly(2029, 3, 6), "Supermercado");
                await DRS.RegistExpenseAsync(520000m, new DateOnly(2029, 3, 18), "Salidas");
                await DRS.RegistIncomeAsync(1850000m, new DateOnly(2029, 3, 10), "Trading");
                await DRS.RegistExpenseAsync(780000m, new DateOnly(2029, 3, 23), "Trading");

                await DRS.RegistExpenseAsync(420000m, new DateOnly(2029, 4, 8), "Supermercado");
                await DRS.RegistExpenseAsync(680000m, new DateOnly(2029, 4, 19), "Ropa");
                await DRS.RegistIncomeAsync(1300000m, new DateOnly(2029, 4, 12), "Trading");
                await DRS.RegistIncomeAsync(820000m, new DateOnly(2029, 4, 26), "Freelance");

                await DRS.RegistExpenseAsync(450000m, new DateOnly(2029, 5, 10), "Supermercado");
                await DRS.RegistExpenseAsync(720000m, new DateOnly(2029, 5, 22), "Salidas");
                await DRS.RegistIncomeAsync(2100000m, new DateOnly(2029, 5, 14), "Trading");

                await DRS.RegistExpenseAsync(480000m, new DateOnly(2029, 6, 5), "Supermercado");
                await DRS.RegistExpenseAsync(950000m, new DateOnly(2029, 6, 16), "Vacaciones");
                await DRS.RegistIncomeAsync(1580000m, new DateOnly(2029, 6, 11), "Trading");
                await DRS.RegistExpenseAsync(880000m, new DateOnly(2029, 6, 27), "Trading");

                await DRS.RegistExpenseAsync(520000m, new DateOnly(2029, 7, 7), "Supermercado");
                await DRS.RegistExpenseAsync(1500000m, new DateOnly(2029, 7, 15), "Vacaciones");
                await DRS.RegistIncomeAsync(2400000m, new DateOnly(2029, 7, 18), "Trading");

                await DRS.RegistExpenseAsync(560000m, new DateOnly(2029, 8, 6), "Supermercado");
                await DRS.RegistExpenseAsync(620000m, new DateOnly(2029, 8, 18), "Ropa");
                await DRS.RegistIncomeAsync(1850000m, new DateOnly(2029, 8, 13), "Trading");
                await DRS.RegistIncomeAsync(950000m, new DateOnly(2029, 8, 29), "Trading");

                await DRS.RegistExpenseAsync(600000m, new DateOnly(2029, 9, 8), "Supermercado");
                await DRS.RegistExpenseAsync(480000m, new DateOnly(2029, 9, 20), "Salidas");
                await DRS.RegistIncomeAsync(2200000m, new DateOnly(2029, 9, 11), "Trading");
                await DRS.RegistExpenseAsync(1100000m, new DateOnly(2029, 9, 25), "Trading");

                await DRS.RegistExpenseAsync(640000m, new DateOnly(2029, 10, 9), "Supermercado");
                await DRS.RegistExpenseAsync(820000m, new DateOnly(2029, 10, 21), "Ropa");
                await DRS.RegistIncomeAsync(1950000m, new DateOnly(2029, 10, 14), "Trading");
                await DRS.RegistIncomeAsync(880000m, new DateOnly(2029, 10, 28), "Trading");

                await DRS.RegistExpenseAsync(720000m, new DateOnly(2029, 11, 7), "Supermercado");
                await DRS.RegistExpenseAsync(680000m, new DateOnly(2029, 11, 19), "Salidas");
                await DRS.RegistIncomeAsync(2650000m, new DateOnly(2029, 11, 12), "Trading");

                await DRS.RegistExpenseAsync(880000m, new DateOnly(2029, 12, 10), "Supermercado");
                await DRS.RegistExpenseAsync(1400000m, new DateOnly(2029, 12, 20), "Regalos");
                await DRS.RegistExpenseAsync(950000m, new DateOnly(2029, 12, 28), "Salidas");
                await DRS.RegistIncomeAsync(2400000m, new DateOnly(2029, 12, 14), "Trading");
                await DRS.RegistIncomeAsync(1200000m, new DateOnly(2029, 12, 29), "Aguinaldo");

                // ===== 2030 =====
                await DRS.RegistExpenseAsync(950000m, new DateOnly(2030, 1, 8), "Supermercado");
                await DRS.RegistExpenseAsync(1200000m, new DateOnly(2030, 1, 20), "Salidas");
                await DRS.RegistIncomeAsync(2800000m, new DateOnly(2030, 1, 12), "Trading");
                await DRS.RegistExpenseAsync(1500000m, new DateOnly(2030, 1, 26), "Trading");

                await DRS.RegistExpenseAsync(1100000m, new DateOnly(2030, 2, 10), "Supermercado");
                await DRS.RegistExpenseAsync(880000m, new DateOnly(2030, 2, 21), "Ropa");
                await DRS.RegistIncomeAsync(3200000m, new DateOnly(2030, 2, 14), "Trading");
                await DRS.RegistIncomeAsync(1400000m, new DateOnly(2030, 2, 28), "Trading");

                await DRS.RegistExpenseAsync(1250000m, new DateOnly(2030, 3, 7), "Supermercado");
                await DRS.RegistExpenseAsync(1400000m, new DateOnly(2030, 3, 19), "Salidas");
                await DRS.RegistIncomeAsync(3600000m, new DateOnly(2030, 3, 11), "Trading");
                await DRS.RegistExpenseAsync(1850000m, new DateOnly(2030, 3, 24), "Trading");

                await DRS.RegistExpenseAsync(1400000m, new DateOnly(2030, 4, 9), "Supermercado");
                await DRS.RegistExpenseAsync(1500000m, new DateOnly(2030, 4, 20), "Ropa");
                await DRS.RegistIncomeAsync(2900000m, new DateOnly(2030, 4, 13), "Trading");
                await DRS.RegistIncomeAsync(1800000m, new DateOnly(2030, 4, 27), "Freelance");

                await DRS.RegistExpenseAsync(1550000m, new DateOnly(2030, 5, 11), "Supermercado");
                await DRS.RegistExpenseAsync(1850000m, new DateOnly(2030, 5, 23), "Salidas");
                await DRS.RegistIncomeAsync(4200000m, new DateOnly(2030, 5, 15), "Trading");

                await DRS.RegistExpenseAsync(1700000m, new DateOnly(2030, 6, 6), "Supermercado");
                await DRS.RegistExpenseAsync(2500000m, new DateOnly(2030, 6, 17), "Vacaciones");
                await DRS.RegistIncomeAsync(3400000m, new DateOnly(2030, 6, 12), "Trading");
                await DRS.RegistExpenseAsync(1950000m, new DateOnly(2030, 6, 28), "Trading");

                await DRS.RegistExpenseAsync(1850000m, new DateOnly(2030, 7, 8), "Supermercado");
                await DRS.RegistExpenseAsync(3800000m, new DateOnly(2030, 7, 16), "Vacaciones");
                await DRS.RegistIncomeAsync(4800000m, new DateOnly(2030, 7, 19), "Trading");

                await DRS.RegistExpenseAsync(2000000m, new DateOnly(2030, 8, 7), "Supermercado");
                await DRS.RegistExpenseAsync(1400000m, new DateOnly(2030, 8, 19), "Ropa");
                await DRS.RegistIncomeAsync(3850000m, new DateOnly(2030, 8, 14), "Trading");
                await DRS.RegistIncomeAsync(2100000m, new DateOnly(2030, 8, 30), "Trading");

                await DRS.RegistExpenseAsync(2200000m, new DateOnly(2030, 9, 9), "Supermercado");
                await DRS.RegistExpenseAsync(1200000m, new DateOnly(2030, 9, 21), "Salidas");
                await DRS.RegistIncomeAsync(4500000m, new DateOnly(2030, 9, 12), "Trading");
                await DRS.RegistExpenseAsync(2400000m, new DateOnly(2030, 9, 26), "Trading");

                await DRS.RegistExpenseAsync(2400000m, new DateOnly(2030, 10, 10), "Supermercado");
                await DRS.RegistExpenseAsync(1800000m, new DateOnly(2030, 10, 22), "Ropa");
                await DRS.RegistIncomeAsync(4100000m, new DateOnly(2030, 10, 15), "Trading");
                await DRS.RegistIncomeAsync(1900000m, new DateOnly(2030, 10, 29), "Trading");

                await DRS.RegistExpenseAsync(2700000m, new DateOnly(2030, 11, 8), "Supermercado");
                await DRS.RegistExpenseAsync(1500000m, new DateOnly(2030, 11, 20), "Salidas");
                await DRS.RegistIncomeAsync(5400000m, new DateOnly(2030, 11, 13), "Trading");

                await DRS.RegistExpenseAsync(3200000m, new DateOnly(2030, 12, 11), "Supermercado");
                await DRS.RegistExpenseAsync(2800000m, new DateOnly(2030, 12, 21), "Regalos");
                await DRS.RegistExpenseAsync(2100000m, new DateOnly(2030, 12, 29), "Salidas");
                await DRS.RegistIncomeAsync(5200000m, new DateOnly(2030, 12, 15), "Trading");
                await DRS.RegistIncomeAsync(2500000m, new DateOnly(2030, 12, 30), "Aguinaldo");

                // ===== 2031 Enero =====
                await DRS.RegistExpenseAsync(3500000m, new DateOnly(2031, 1, 8), "Supermercado");
                await DRS.RegistExpenseAsync(1800000m, new DateOnly(2031, 1, 18), "Salidas");
                await DRS.RegistIncomeAsync(4800000m, new DateOnly(2031, 1, 12), "Trading");

                Console.WriteLine("Termine de registrar todo");
            }
        }
    }
        
}
