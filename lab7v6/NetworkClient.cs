using System;
using System.Net.Http;

public class NetworkClient
{
    private int _attempt = 0;

    public void SendLogToServer(string url, string logContent)
    {
        _attempt++;

        // Імітуємо 4 HttpRequestException
        if (_attempt <= 4)
        {
            Console.WriteLine("[NetworkClient] Імітація HttpRequestException");
            throw new HttpRequestException("Помилка мережевого запиту.");
        }

        Console.WriteLine($"[NetworkClient] Лог успішно відправлено на {url}");
    }
}
