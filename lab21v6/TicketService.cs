public class TicketService
{
    public decimal CalculateTotalPrice(decimal basePrice, int seats, ITicketStrategy strategy)
    {
        return strategy.CalculatePrice(basePrice, seats);
    }
}
