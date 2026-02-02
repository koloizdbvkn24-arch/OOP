# Лабораторна робота: Liskov Substitution Principle (LSP) на прикладі Vehicle & ElectricCar

## Мета роботи

- Вивчити принцип Liskov Substitution Principle (LSP) з SOLID.
- Продемонструвати порушення LSP у початковій ієрархії класів.
- Реалізувати альтернативне рішення, що дотримується LSP за допомогою композиції.

---

## 1. Початкова ієрархія класів (порушує LSP)

### Опис

- Базовий клас `Vehicle` має метод `Refuel()`, який заповнює паливний бак.
- Підклас `ElectricCar` не заправляється пальним, тому перевизначає метод `Refuel()` і кидає `NotImplementedException`.

### Код

```csharp
public class Vehicle
{
    public virtual void Refuel()
    {
        Console.WriteLine("Транспорт заправлено пальним!");
    }
}

public class ElectricCar : Vehicle
{
    public override void Refuel()
    {
        throw new NotImplementedException();
    }
}

static void RefuelVehicle(Vehicle vehicle)
{
    vehicle.Refuel();
}

class Program
{
    static void Main()
    {
        Vehicle car = new Vehicle();
        RefuelVehicle(car); // працює

        ElectricCar eCar = new ElectricCar();
        RefuelVehicle(eCar); // ❌ кине NotImplementedException
    }
}
```

### Аналіз порушення LSP

- Підклас `ElectricCar` порушує Liskov Substitution Principle. Метод `Refuel()` базового класу гарантує роботу для всіх транспортних засобів.
- Клієнтський код, який працює з базовим класом (`RefuelVehicle`), стає небезпечним і може ламатися.

---

## 2. Альтернативне рішення (композиція)

### Ідея

- Виділяємо поведінку заправки/зарядки в окремий інтерфейс `IFuelBehavior`.
- Базовий клас `Vehicle` делегує роботу цьому об’єкту.
- Підкласи підставляють конкретну реалізацію (`GasolineRefuel` або `ElectricCharge`).
- Клієнтський код завжди працює, LSP дотримано.

### Код

```csharp
// Інтерфейс поведінки
public interface IFuelBehavior
{
    void Refuel();
}

// Конкретні реалізації
public class GasolineRefuel : IFuelBehavior
{
    public void Refuel() => Console.WriteLine("Транспорт заправлено пальним!");
}

public class ElectricCharge : IFuelBehavior
{
    public void Refuel() => Console.WriteLine("Електромобіль заряджено!");
}

// Базовий клас з композицією
public abstract class Vehicle
{
    protected IFuelBehavior fuelBehavior;

    protected Vehicle(IFuelBehavior behavior)
    {
        fuelBehavior = behavior;
    }

    public void PerformRefuel() => fuelBehavior.Refuel();
}

// Конкретні класи
public class Car : Vehicle
{
    public Car() : base(new GasolineRefuel()) {}
}

public class ElectricCar : Vehicle
{
    public ElectricCar() : base(new ElectricCharge()) {}
}
}

// Клієнтський метод
static void RefuelVehicle(Vehicle vehicle)
{
    vehicle.PerformRefuel();
}

// Main
class Program
{
    static void Main()
    {
        Vehicle car = new Car();
        RefuelVehicle(car); // "Транспорт заправлено пальним!"

        Vehicle eCar = new ElectricCar();
        RefuelVehicle(eCar); // "Електромобіль заряджено!"
    }
}
```

---

## 3. Пояснення

- **Початкова ієрархія:** порушує LSP через кидання винятку у підкласі.
- **Альтернативне рішення:**
  - Використано **композицію**: клас `Vehicle` містить об’єкт `IFuelBehavior`.
  - Підкласи передають конкретну реалізацію поведінки.
  - Метод `PerformRefuel()` завжди працює → LSP дотримано.

- Такий підхід забезпечує:
  - гнучкість (легко додавати нові типи поведінки),
  - масштабованість,
  - безпечний та універсальний клієнтський код.

---

## 4. Висновок

- Порушення LSP призводить до нестабільності коду та необхідності додаткових перевірок підкласів.
- Використання композиції або стратегії дозволяє делегувати поведінку, дотримуючись принципів SOLID.
- Код стає більш чистим, масштабованим і безпечним для повторного використання.
