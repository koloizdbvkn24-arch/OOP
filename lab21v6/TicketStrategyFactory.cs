public static class TicketStrategyFactory
{
    public static ITicketStrategy CreateStrategy(string ticketType)
    {
        return ticketType.ToLower() switch
        {
            "regular" => new RegularTicket(),
            "student" => new StudentTicket(),
            "vip" => new VipTicket(),
            _ => throw new ArgumentException("Unknown ticket type")
        };
    }
}
