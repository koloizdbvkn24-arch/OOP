public class OrderService
{
    private readonly IKitchen _kitchen;
    private readonly IDelivery _delivery;
    private readonly IBilling _billing;

    public OrderService(IKitchen kitchen, IDelivery delivery, IBilling billing)
    {
        _kitchen = kitchen;
        _delivery = delivery;
        _billing = billing;
    }

    public void PlaceOrder(string orderDetails, string address)
    {
        _kitchen.Prepare(orderDetails);
        _delivery.Deliver(orderDetails, address);
        _billing.Generate(orderDetails);
    }

    public void NotifyKitchen(string orderDetails) => _kitchen.Prepare(orderDetails);

    public void DeliverOrder(string orderDetails, string address) =>
        _delivery.Deliver(orderDetails, address);

    public void GenerateBill(string orderDetails) => _billing.Generate(orderDetails);
}
