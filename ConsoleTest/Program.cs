using Repositories;
using DomainModel;
using System.IO;
using Microsoft.VisualBasic;
using DataServices;

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
            var today = DateOnly.FromDateTime(DateTime.Today);

            Console.WriteLine($"Total expenses: {DPS.Expenses():N2}");
            Console.WriteLine($"Total income: {DPS.Income():N2}");
            Console.WriteLine($"Final net: {DPS.Net:N2}\n ¿you are losing more that you are ganing? {DPS.Deficit}");
        }
    }
}
