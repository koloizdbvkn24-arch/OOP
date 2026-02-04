public class MockKitchen : IKitchen
{
    public void Prepare(string orderDetails) =>
        Console.WriteLine($"[Mock] Готуємо замовлення: {orderDetails}");
}

public class MockDelivery : IDelivery
{
    public void Deliver(string orderDetails, string address) =>
        Console.WriteLine($"[Mock] Доставляємо замовлення: {orderDetails} за адресою: {address}");
}

public class MockBilling : IBilling
{
    public void Generate(string orderDetails) =>
        Console.WriteLine($"[Mock] Генеруємо рахунок для замовлення: {orderDetails}");
}
