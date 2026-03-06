# ЗВІТ

## з лабораторної роботи

### Тема: Інтеграція патернів Factory Method, Singleton, Strategy та Observer

---

## 1. Мета роботи

Метою лабораторної роботи є реалізація інтегрованої системи на мові C#, що одночасно використовує патерни:

- Factory Method
- Singleton
- Strategy
- Observer

Також необхідно продемонструвати їх взаємодію та реалізувати сценарії динамічної зміни логера і алгоритму обробки даних.

---

## 2. Завдання

1. Реалізувати систему логування:
   - `ILogger`
   - `ConsoleLogger`
   - `FileLogger`
2. Реалізувати фабрики логерів:
   - `LoggerFactory`
   - `ConsoleLoggerFactory`
   - `FileLoggerFactory`
3. Реалізувати `LoggerManager` як Singleton.
4. Реалізувати стратегії:
   - `IDataProcessorStrategy`
   - `EncryptDataStrategy`
   - `CompressDataStrategy`
5. Реалізувати `DataContext`.
6. Реалізувати `DataPublisher` з подією `DataProcessed`.
7. Реалізувати `ProcessingLoggerObserver`.
8. Продемонструвати 3 сценарії в методі `Main`.

---

## 3. Теоретичні відомості

### 3.1 Factory Method

Factory Method — породжувальний патерн, який інкапсулює створення об’єктів та дозволяє підміняти їх конкретні реалізації без зміни клієнтського коду.

---

### 3.2 Singleton

Singleton — породжувальний патерн, що гарантує існування лише одного екземпляра класу та надає глобальну точку доступу до нього.

У роботі реалізовано `LoggerManager`.

---

### 3.3 Strategy

Strategy — поведінковий патерн, що дозволяє змінювати алгоритм під час виконання програми.

У роботі:

- `EncryptDataStrategy`
- `CompressDataStrategy`

---

### 3.4 Observer

Observer — поведінковий патерн, який реалізує механізм підписки на події.

У системі:

- `DataPublisher` — суб’єкт
- `ProcessingLoggerObserver` — спостерігач

---

## 4. Архітектура системи

```
DataContext
     │
     ▼
IDataProcessorStrategy
     │
     ▼
DataPublisher (подія DataProcessed)
     │
     ▼
ProcessingLoggerObserver
     │
     ▼
LoggerManager (Singleton)
     │
     ▼
ILogger (ConsoleLogger / FileLogger)
```

---

## 5. Опис реалізації

### 5.1 Логування

- `ILogger` — інтерфейс логування.
- `ConsoleLogger` — логування в консоль.
- `FileLogger` — логування у файл.
- `LoggerFactory` — абстрактна фабрика.
- `ConsoleLoggerFactory`, `FileLoggerFactory` — конкретні фабрики.
- `LoggerManager` — Singleton для централізованого керування логуванням.

---

### 5.2 Обробка даних

- `IDataProcessorStrategy` — інтерфейс стратегії.
- `EncryptDataStrategy` — кодування в Base64.
- `CompressDataStrategy` — видалення пробілів.
- `DataContext` — контекст, який використовує поточну стратегію.

---

### 5.3 Події

- `DataPublisher` генерує подію `DataProcessed`.
- `ProcessingLoggerObserver` підписується на подію та виконує логування через `LoggerManager`.

---

## 6. Сценарії демонстрації

### Сценарій 1 — Повна інтеграція

1. Ініціалізація `LoggerManager` через `ConsoleLoggerFactory`.
2. Створення `DataContext` з `EncryptDataStrategy`.
3. Підписка `ProcessingLoggerObserver`.
4. Обробка даних та публікація події.

**Результат:** логування відбувається в консоль.

---

### Сценарій 2 — Динамічна зміна логера

1. Після першої обробки викликати:
   ```csharp
   LoggerManager.Initialize(new FileLoggerFactory());
   ```
2. Повторити обробку.

**Результат:** логування переходить у файл `log.txt`.

---

### Сценарій 3 — Динамічна зміна стратегії

1. Викликати:
   ```csharp
   dataContext.SetStrategy(new CompressDataStrategy());
   ```
2. Повторити обробку.

**Результат:** обробка відбувається за новим алгоритмом.

---

## 7. Висновки

У ході виконання лабораторної роботи було реалізовано інтегровану систему з використанням чотирьох патернів проєктування.

- Factory Method забезпечує гнучке створення логерів.
- Singleton гарантує централізоване керування логуванням.
- Strategy дозволяє змінювати алгоритм обробки під час виконання.
- Observer забезпечує слабке зв’язування через механізм подій.

Реалізовані сценарії підтвердили коректну взаємодію патернів та можливість динамічної зміни поведінки системи.
