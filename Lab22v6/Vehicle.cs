public abstract class Vehicle
{
    protected IFuelBehavior fuelBehavior;

    protected Vehicle(IFuelBehavior behavior)
    {
        fuelBehavior = behavior;
    }

    public void PerformRefuel()
    {
        fuelBehavior.Refuel();
    }
}

public class Car : Vehicle
{
    public Car() : base(new GasolineRefuel()) {}
}

public class ElectricCar : Vehicle
{
    public ElectricCar() : base(new ElectricCharge()) {}
}

