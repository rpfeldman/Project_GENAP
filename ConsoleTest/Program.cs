using Repositories;
using DomainModel;
using System.IO;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var path = "test.db";

            var repo = new EF_SQLite_StateStorage(path);

            decimal Expenses = 0m;
            decimal Income = 0m;

            repo.Save(5000, DateOnly.FromDateTime(DateTime.Today), Category.Social, true);
            repo.Save(100000, DateOnly.FromDateTime(DateTime.Today), Category.Clothes,true);
            repo.Save(1000000, DateOnly.FromDateTime(DateTime.Today), Category.Trades, false);

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
