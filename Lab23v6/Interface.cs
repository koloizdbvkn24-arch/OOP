public interface IKitchen
{
    void Prepare(string orderDetails);
}

public interface IDelivery
{
    void Deliver(string orderDetails, string address);
}

public interface IBilling
{
    void Generate(string orderDetails);
}
