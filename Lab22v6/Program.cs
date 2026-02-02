// using System;

// // Базовий клас
// public class Vehicle
// {
//     public virtual void Refuel()
//     {
//         Console.WriteLine("Транспорт заправлено пальним!");
//     }
// }

// // Похідний клас
// public class ElectricCar : Vehicle
// {
//     // Порушення LSP: електромобіль не заправляється пальним
//     public override void Refuel()
//     {
//         throw new NotImplementedException();
//     }
// }


// class Program
// {
//     static void RefuelVehicle(Vehicle vehicle)
//     {
//         vehicle.Refuel(); // Очікує, що об'єкт можна "заправити"
//     }

//     static void Main()
//     {
//         Vehicle car = new Vehicle();
//         RefuelVehicle(car);

//         ElectricCar eCar = new ElectricCar();
//         RefuelVehicle(eCar); 
//     }
// }




class Program
{
    static void Main()
    {
        Vehicle car = new Car();
        RefuelVehicle(car); // "Транспорт заправлено пальним!"

        Vehicle eCar = new ElectricCar();
        RefuelVehicle(eCar); // "Електромобіль заряджено!"
    }

    static void RefuelVehicle(Vehicle vehicle)
{
    vehicle.PerformRefuel(); // завжди працює
}
}

