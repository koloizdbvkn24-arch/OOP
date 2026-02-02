// Інтерфейс заправки / зарядки
public interface IFuelBehavior
{
    void Refuel();
}

// Конкретні реалізації
public class GasolineRefuel : IFuelBehavior
{
    public void Refuel()
    {
        Console.WriteLine("Транспорт заправлено пальним!");
    }
}

public class ElectricCharge : IFuelBehavior
{
    public void Refuel()
    {
        Console.WriteLine("Електромобіль заряджено!");
    }
}
