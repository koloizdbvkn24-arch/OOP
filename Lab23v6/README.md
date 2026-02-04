# OrderService: Аналіз поганого та хорошого варіантів

## Завдання

Клас **OrderService** обробляє замовлення, готує їжу (сповіщає кухню), доставляє та виставляє рахунок.
**Залежності:** KitchenSystem, DeliveryMap, BillingSystem

**Проблема:** Змішування логіки та прямі залежності від служб.

---

## Поганий варіант

### Особливості

- Один великий інтерфейс `IOrderService` робить **все одразу**:
  - PlaceOrder, NotifyKitchen, DeliverOrder, GenerateBill

- `OrderService` **сам створює залежності**:

  ```csharp
  private KitchenSystem _kitchen = new KitchenSystem();
  private DeliveryMap _delivery = new DeliveryMap();
  private BillingSystem _billing = new BillingSystem();
  ```

- Неможливо легко протестувати або замінити реалізації.

### Проблеми

1. **Порушено ISP** — жирний інтерфейс, що робить багато роботи.
2. **Порушено DIP** — клас залежить від конкретних реалізацій (`new KitchenSystem()`), а не від інтерфейсів.
3. **Тестування складне** — потрібні реальні сервіси для перевірки.

---

## Хороший варіант

### Рішення

1. Виділено вузькі інтерфейси (ISP):

```csharp
public interface IKitchen { void Prepare(string orderDetails); }
public interface IDelivery { void Deliver(string orderDetails, string address); }
public interface IBilling { void Generate(string orderDetails); }
```

2. Використано **Constructor Injection** (DIP) у `OrderService`:

```csharp
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
}
```

3. Заглушки для тестування:

```csharp
public class MockKitchen : IKitchen { ... }
public class MockDelivery : IDelivery { ... }
public class MockBilling : IBilling { ... }
```

### Переваги

- **ISP дотримано** — кожен сервіс відповідає за свою вузьку сферу.
- **DIP дотримано** — OrderService не створює залежності, отримує їх через конструктор.
- **Легко тестувати** — можна підставляти mock-реалізації.
- **Масштабованість** — легко додавати нові сервіси (наприклад, push-сповіщення).

---

## Висновок

Поганий варіант порушує принципи SOLID, змішує логіку і створює прямі залежності.
Хороший варіант дотримується **ISP та DIP**, робить код **чистим, тестованим та масштабованим**.
