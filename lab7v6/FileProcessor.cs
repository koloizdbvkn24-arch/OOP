using System;
using System.IO;

public class FileProcessor
{
    private int _attempt = 0;

    public string GetLogContent(string path)
    {
        _attempt++;

        // Імітуємо помилки перші 2 рази FileNotFoundException
        if (_attempt <= 2)
        {
            Console.WriteLine("[FileProcessor] Імітація FileNotFoundException");
            throw new FileNotFoundException("Файл не знайдено.");
        }

        return "Log content: system operational.";
    }
}
