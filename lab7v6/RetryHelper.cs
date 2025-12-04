using System;
using System.Threading;

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool> shouldRetry = null!)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(200);

        int attempt = 0;

        while (true)
        {
            try
            {
                attempt++;
                Console.WriteLine($"[Retry] Спроба {attempt}/{retryCount}");
                return operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Retry] Помилка: {ex.GetType().Name} — {ex.Message}");

                if (attempt >= retryCount)
                {
                    Console.WriteLine("[Retry] Досягнуто максимум спроб. Викидаю виняток...");
                    throw;
                }

                if (shouldRetry != null && !shouldRetry(ex))
                {
                    Console.WriteLine("[Retry] shouldRetry заборонив повторення. Викидаю...");
                    throw;
                }

                // Експоненційна затримка: delay * 2^(attempt-1)
                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"[Retry] Очікування перед повторною спробою: {delay.TotalMilliseconds}ms\n");

                Thread.Sleep(delay);
            }
        }
    }
}
