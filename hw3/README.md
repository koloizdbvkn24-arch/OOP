# Принципи ISP та DIP

## Вступ

Принципи **ISP (Interface Segregation Principle)** та **DIP (Dependency Inversion Principle)** є частиною SOLID і спрямовані на підвищення гнучкості та підтримуваності коду. Вони допомагають створювати модульні системи, де класи менше залежать один від одного.

## Interface Segregation Principle (ISP)

Принцип розділення інтерфейсів (ISP) говорить: _«Клієнти не повинні залежати від інтерфейсів, які вони не використовують»._

### Приклад порушення ISP

```csharp
public interface IPrinter
{
    void Print(Document doc);
    void Scan(Document doc);
    void Fax(Document doc);
}

public class OldPrinter : IPrinter
{
    public void Print(Document doc) { /* реалізація */ }
    public void Scan(Document doc) { throw new NotImplementedException(); }
    public void Fax(Document doc) { throw new NotImplementedException(); }
}
```

**Проблема:** OldPrinter реалізує методи, які не підтримує, що порушує ISP.

### Вирішення проблеми

```csharp
public interface IPrinter
{
    void Print(Document doc);
}

public interface IScanner
{
    void Scan(Document doc);
}

public class OldPrinter : IPrinter
{
    public void Print(Document doc) { /* реалізація */ }
}
```

Тепер кожен клас реалізує лише потрібні інтерфейси.

## Dependency Inversion Principle (DIP)

Принцип інверсії залежностей (DIP) говорить: _«Модулі високого рівня не повинні залежати від модулів низького рівня. Обидва повинні залежати від абстракцій»._

### Переваги DIP через Dependency Injection

- **Зменшує зв'язність коду:** високорівневі класи залежать від інтерфейсів, а не від конкретних реалізацій.
- **Полегшує тестування:** можна підміняти реальні залежності моками або стабами.
- **Підвищує гнучкість:** легше змінювати реалізацію залежностей без модифікації основного коду.

```csharp
public interface IMessageService
{
    void SendMessage(string message);
}

public class EmailService : IMessageService
{
    public void SendMessage(string message) { /* реалізація */ }
}

public class Notification
{
    private readonly IMessageService _service;
    public Notification(IMessageService service)
    {
        _service = service;
    }
    public void Notify(string message) { _service.SendMessage(message); }
}
```

Тепер Notification не залежить від конкретного EmailService і можна підміняти реалізацію.

## Як ISP сприяє кращому DI та тестуванню

- **Вузькі інтерфейси** дозволяють передавати в класи лише ті залежності, які дійсно потрібні.
- **Модульність:** класи легко замінювати на мок-реалізації для тестів.
- **Чіткі обов'язки:** полегшує впровадження DI, оскільки кожна залежність виконує конкретну роль.

## Висновок

Застосування ISP та DIP сприяє створенню більш гнучких, тестованих і підтримуваних систем. Розділення інтерфейсів запобігає зайвим залежностям, а інверсія залежностей через DI дозволяє легко змінювати реалізації без впливу на високорівневу логіку.
