using Repositories;
using DomainModel;
using System.IO;
using Microsoft.VisualBasic;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var path = "test.db";
            var repo = new EF_SQLite_StateStorage(path, [14, 2]);
            var today = DateOnly.FromDateTime(DateTime.Today);

            decimal Expenses = 0m;
            decimal Income = 0m;

            repo.ClearStorage();
            repo.Save(1000, today, "🤡😭🦁", true);

            foreach (var item in repo.GetAll())
            {
                Console.Write($"[{item.TransactionId}] ");
                Console.Write(item.Depletion ? "Se gasto " : "se gano ");
                Console.Write($"{item.Value.ToString("N2")}$ en {item.Category} el {item.Date}\n\n");

                Expenses += item.Depletion ? item.Value : 0;
                Income += item.Depletion ? 0 : item.Value;
            }

            var FinalResult = Income - Expenses;
            Console.WriteLine($"Se gasto: {Expenses.ToString("N2")}$\n");
            Console.WriteLine($"Se Gano: {Income.ToString("N2")}$\n");
            Console.WriteLine($"Blance final: {FinalResult.ToString("N2")}$");
        }
    }
}
