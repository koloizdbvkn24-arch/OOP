using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_Delegates
{
    // Делегат
    public delegate bool OrderFilter(Order order);

    public class Order
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string? Status { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення колекції замовлень
            List<Order> orders = new List<Order>
            {
                new Order { Id = 1, Amount = 1500, Status = "Completed" },
                new Order { Id = 2, Amount = 900, Status = "Pending" },
                new Order { Id = 3, Amount = 2500, Status = "Completed" },
                new Order { Id = 4, Amount = 400, Status = "Canceled" },
                new Order { Id = 5, Amount = 1200, Status = "Pending" },
            };


            OrderFilter completedFilter = delegate (Order o)
            {
                return o.Status == "Completed";
            };

            var completedOrders = orders.Where(o => completedFilter(o));
            Console.WriteLine("=== Completed Orders (Delegate) ===");
            foreach (var o in completedOrders)
                Console.WriteLine($"Order {o.Id}: {o.Amount}");

            Predicate<Order> isPending = o => o.Status == "Pending";

            int pendingCount = orders.FindAll(isPending).Count;

            Console.WriteLine($"\nPending count (Predicate): {pendingCount}");

            Func<List<Order>, double> completedSum = list =>
                list.Where(o => o.Status == "Completed")
                    .Sum(o => o.Amount);

            Console.WriteLine($"Completed orders total sum (Func): {completedSum(orders)}");

            Action<Order> printOrder = o =>
                Console.WriteLine($"ID: {o.Id}, Amount: {o.Amount}, Status: {o.Status}");

            Console.WriteLine("\n=== All Orders (Action) ===");
            orders.ForEach(printOrder);

            var sortedByAmount = orders.OrderBy(o => o.Amount);

            Console.WriteLine("\n=== Sorted by Amount ===");
            foreach (var o in sortedByAmount)
                Console.WriteLine($"Order {o.Id}: {o.Amount}");

            double totalAll = orders.Aggregate(0.0, (acc, o) => acc + o.Amount);

            Console.WriteLine($"\nTotal amount of ALL orders (Aggregate): {totalAll}");
        }
    }
}
