using System;

namespace lab20
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Order(int id, string customerName, decimal totalAmount)
        {
            Id = id;
            CustomerName = customerName;
            TotalAmount = totalAmount;
            Status = OrderStatus.New;
        }
    }

    public enum OrderStatus
    {
        New,
        PendingValidation,
        Processed,
        Shipped,
        Delivered,
        Cancelled
    }

    // Порушує SRP
    public class OrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            // Валідація
            if (order.TotalAmount <= 0)
            {
                Console.WriteLine($"Order {order.Id} is invalid. TotalAmount must be > 0.");
                order.Status = OrderStatus.Cancelled;
                return;
            }

            order.Status = OrderStatus.PendingValidation;

            // Збереження в базу
            Console.WriteLine($"Order {order.Id} saved to database.");

            // Відправка email
            Console.WriteLine($"Email sent to {order.CustomerName} confirming the order.");

            // Оновлення статусу
            order.Status = OrderStatus.Processed;
            Console.WriteLine($"Order {order.Id} status updated to {order.Status}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var order1 = new Order(1, "Alice", 100);
            var order2 = new Order(2, "Bob", -50);

            var processor = new OrderProcessor();
            processor.ProcessOrder(order1);
            processor.ProcessOrder(order2);

            Console.ReadLine();
        }
    }
}
