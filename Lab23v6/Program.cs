// // Поганий інтерфейс — "жирний", порушує ISP
// public interface IOrderService
// {
//     void PlaceOrder(string orderDetails);
//     void NotifyKitchen(string orderDetails);
//     void DeliverOrder(string orderDetails, string address);
//     void GenerateBill(string orderDetails);
// }

// // Поганий клас — порушує DIP
// public class OrderService : IOrderService
// {
//     private KitchenSystem _kitchen = new KitchenSystem();
//     private DeliveryMap _delivery = new DeliveryMap();
//     private BillingSystem _billing = new BillingSystem();

//     public void PlaceOrder(string orderDetails)
//     {
//         // Всі дії всередині класу
//         _kitchen.Prepare(orderDetails);
//         _delivery.Deliver(orderDetails, "Some Address");
//         _billing.Generate(orderDetails);
//     }

//     public void NotifyKitchen(string orderDetails)
//     {
//         _kitchen.Prepare(orderDetails);
//     }

//     public void DeliverOrder(string orderDetails, string address)
//     {
//         _delivery.Deliver(orderDetails, address);
//     }

//     public void GenerateBill(string orderDetails)
//     {
//         _billing.Generate(orderDetails);
//     }
// }

// // Зовнішні системи
// public class KitchenSystem
// {
//     public void Prepare(string orderDetails) =>
//         Console.WriteLine($"Готуємо замовлення: {orderDetails}");
// }

// public class DeliveryMap
// {
//     public void Deliver(string orderDetails, string address) =>
//         Console.WriteLine($"Доставляємо замовлення: {orderDetails} за адресою: {address}");
// }

// public class BillingSystem
// {
//     public void Generate(string orderDetails) =>
//         Console.WriteLine($"Генеруємо рахунок для замовлення: {orderDetails}");
// }
