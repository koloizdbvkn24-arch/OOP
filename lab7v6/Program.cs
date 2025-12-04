using System;
using System.IO;
using System.Net.Http;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Lab 7: Retry + Exceptions ===\n");

        var fileProcessor = new FileProcessor();
        var networkClient = new NetworkClient();

        // shouldRetry повторювати лише для FileNotFoundException та HttpRequestException
        Func<Exception, bool> shouldRetry = ex =>
            ex is FileNotFoundException || ex is HttpRequestException;

        try
        {
            // Отримання логів з файлу з Retry
            string logContent = RetryHelper.ExecuteWithRetry(
                () => fileProcessor.GetLogContent("logs.txt"),
                retryCount: 5,
                initialDelay: TimeSpan.FromMilliseconds(300),
                shouldRetry: shouldRetry
            );

            Console.WriteLine($"\n[INFO] FileProcessor успішно повернув лог: \"{logContent}\"\n");

            // Відправка на сервер з Retry
            RetryHelper.ExecuteWithRetry(
                () =>
                {
                    networkClient.SendLogToServer("https://server.com/upload", logContent);
                    return true; 
                },
                retryCount: 6,
                initialDelay: TimeSpan.FromMilliseconds(300),
                shouldRetry: shouldRetry
            );

            Console.WriteLine("\n[INFO] NetworkClient успішно відправив лог!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FATAL] Операцію не вдалося завершити: {ex.Message}");
        }
    }
}
