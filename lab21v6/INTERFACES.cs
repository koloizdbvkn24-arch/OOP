using System;

// Інтерфейс стратегії квитка
public interface ITicketStrategy
{
    decimal CalculatePrice(decimal basePrice, int seats);
}

// Regular Ticket — без змін
public class RegularTicket : ITicketStrategy
{
    public decimal CalculatePrice(decimal basePrice, int seats)
    {
        return basePrice * seats;
    }
}

// Student Ticket — знижка 20%
public class StudentTicket : ITicketStrategy
{
    public decimal CalculatePrice(decimal basePrice, int seats)
    {
        return basePrice * seats * 0.8m;
    }
}

// VIP Ticket — націнка 50%
public class VipTicket : ITicketStrategy
{
    public decimal CalculatePrice(decimal basePrice, int seats)
    {
        return basePrice * seats * 1.5m;
    }
}
