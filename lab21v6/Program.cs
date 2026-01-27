class Program
{
    static void Main()
    {
        Console.WriteLine("Cinema Ticket Demo:");

        Console.Write("Enter ticket type (Regular, Student, VIP): ");
        string ticketType = Console.ReadLine();

        Console.Write("Enter base price per seat: ");
        decimal basePrice = decimal.Parse(Console.ReadLine());

        Console.Write("Enter number of seats: ");
        int seats = int.Parse(Console.ReadLine());

        ITicketStrategy strategy = TicketStrategyFactory.CreateStrategy(ticketType);
        var service = new TicketService();

        decimal totalPrice = service.CalculateTotalPrice(basePrice, seats, strategy);
        Console.WriteLine($"Total ticket price: {totalPrice:C}");
    }
}
