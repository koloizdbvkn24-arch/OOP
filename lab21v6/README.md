# Lab20: Cinema Ticket Pricing (OCP Demonstration)

## Опис

Ця програма демонструє принцип **Open/Closed Principle (OCP)** на прикладі системи розрахунку вартості квитків у кіно.

Програма дозволяє обирати різні типи квитків і розраховує загальну вартість замовлення:

- **RegularTicket** — стандартний квиток без змін ціни.
- **StudentTicket** — зі знижкою 20%.
- **VipTicket** — з націнкою 50%.

Нові типи квитків можна додавати, не змінюючи логіку сервісу, що демонструє дотримання OCP.

---

## Як працює програма

1. Користувач вводить тип квитка (`Regular`, `Student`, `VIP`).
2. Вводить базову ціну одного квитка.
3. Вводить кількість місць.
4. Програма створює відповідну стратегію через фабрику `TicketStrategyFactory`.
5. `TicketService` обчислює загальну вартість замовлення за обраною стратегією.
6. Виводиться результат у консоль.

---

## Приклад роботи

Cinema Ticket Demo:
Enter ticket type (Regular, Student, VIP): Student
Enter base price per seat: 100
Enter number of seats: 3
Total ticket price: 240.00

- Тут студент отримує 20% знижку: 100 _ 3 _ 0.8 = 240.

Cinema Ticket Demo:
Enter ticket type (Regular, Student, VIP): VIP
Enter base price per seat: 100
Enter number of seats: 2
Total ticket price: 300.00

- VIP квиток має націнку 50%: 100 _ 2 _ 1.5 = 300.

---

## Основні принципи OCP у програмі

- `TicketService` працює тільки з інтерфейсом `ITicketStrategy`.
- Нові стратегії (типи квитків) можна додавати без змін класу сервісу.
- Фабрика `TicketStrategyFactory` дозволяє обирати стратегію по рядку.

---

## Використані класи

- `ITicketStrategy` — інтерфейс для стратегії розрахунку вартості квитка.
- `RegularTicket`, `StudentTicket`, `VipTicket` — реалізації стратегії.
- `TicketStrategyFactory` — фабрика для створення стратегії.
- `TicketService` — сервіс для обчислення загальної вартості квитків.
- `Program` — точка входу і демонстрація роботи програми.

---

## Автор

Лабораторна робота **Lab20**: демонстрація принципу OCP у C#.
