using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#region ===== Lab 17: Logger + Factory Method + Singleton =====

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
        => Console.WriteLine($"[Console] {DateTime.Now:HH:mm:ss} | {message}");
}

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(string message)
    {
        var line = $"[File] {DateTime.Now:HH:mm:ss} | {message}{Environment.NewLine}";
        File.AppendAllText(_filePath, line, Encoding.UTF8);
    }
}

public abstract class LoggerFactory
{
    public abstract ILogger CreateLogger();
}

public class ConsoleLoggerFactory : LoggerFactory
{
    public override ILogger CreateLogger() => new ConsoleLogger();
}

public class FileLoggerFactory : LoggerFactory
{
    private readonly string _filePath;

    public FileLoggerFactory(string filePath = "log.txt")
    {
        _filePath = filePath;
    }

    public override ILogger CreateLogger() => new FileLogger(_filePath);
}

/// <summary>
/// LoggerManager (Singleton) - централізований доступ до логування.
/// Важливо: дозволяє "динамічно змінювати" фабрику (і логер) під час роботи.
/// </summary>
public sealed class LoggerManager
{
    private static readonly object _lock = new object();
    private static LoggerManager? _instance;

    private ILogger _logger;

    private LoggerManager(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Ініціалізація або заміна логера через фабрику.
    /// Використовується в сценарії 1 (init) і сценарії 2 (динамічна зміна).
    /// </summary>
    public static void Initialize(LoggerFactory factory)
    {
        lock (_lock)
        {
            if (_instance == null)
                _instance = new LoggerManager(factory.CreateLogger());
            else
                _instance._logger = factory.CreateLogger(); // ✅ перемикаємо логер
        }
    }

    public static LoggerManager Instance
    {
        get
        {
            if (_instance == null)
                throw new InvalidOperationException("LoggerManager not initialized. Call LoggerManager.Initialize(factory) first.");
            return _instance;
        }
    }

    public void Log(string message) => _logger.Log(message);
}

#endregion

#region ===== Lab 18: Strategy + DataContext + Publisher + Observer =====

public interface IDataProcessorStrategy
{
    string Name { get; }
    string Process(string data);
}

public class EncryptDataStrategy : IDataProcessorStrategy
{
    public string Name => "Encrypt(Base64)";

    public string Process(string data)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
}

/// <summary>
/// "Compress" як навчальний приклад:
/// 1) варіант RemoveSpaces: прибирає всі пробіли
/// або можна зробити NormalizeSpaces (залишати 1 пробіл між словами)
/// </summary>
public class CompressDataStrategy : IDataProcessorStrategy
{
    public string Name => "Compress(RemoveSpaces)";

    public string Process(string data)
        => data.Replace(" ", "");
    // Якщо треба нормалізувати пробіли:
    // => Regex.Replace(data.Trim(), @"\s+", " ");
}

/// <summary>
/// DataPublisher (Subject) - має подію DataProcessed.
/// </summary>
public class DataPublisher
{
    public event Action<string, string>? DataProcessed; 
    // (processedData, strategyName)

    public void PublishDataProcessed(string processedData, string strategyName)
        => DataProcessed?.Invoke(processedData, strategyName);
}

/// <summary>
/// DataContext - приймає стратегію (і може змінювати її).
/// Обробляє дані, але ПУБЛІКАЦІЮ робимо окремо в Main (як в умові).
/// </summary>
public class DataContext
{
    private IDataProcessorStrategy _strategy;

    public DataContext(IDataProcessorStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(IDataProcessorStrategy strategy)
        => _strategy = strategy;

    public string CurrentStrategyName => _strategy.Name;

    public string Process(string input)
        => _strategy.Process(input);
}

/// <summary>
/// ProcessingLoggerObserver - підписується на DataPublisher.DataProcessed
/// і логує через LoggerManager (Singleton).
/// </summary>
public class ProcessingLoggerObserver
{
    public void Subscribe(DataPublisher publisher)
        => publisher.DataProcessed += OnDataProcessed;

    public void Unsubscribe(DataPublisher publisher)
        => publisher.DataProcessed -= OnDataProcessed;

    private void OnDataProcessed(string processedData, string strategyName)
    {
        LoggerManager.Instance.Log($"Observer: DataProcessed via '{strategyName}' -> {processedData}");
    }
}

#endregion

public class Program
{
    public static void Main()
    {
        // =========================
        // СЦЕНАРІЙ 1: Повна інтеграція
        // =========================
        Console.WriteLine("=== Scenario 1: Full integration (Console Logger + Encrypt) ===");

        LoggerManager.Initialize(new ConsoleLoggerFactory());

        var context1 = new DataContext(new EncryptDataStrategy());
        var publisher1 = new DataPublisher();
        var observer1 = new ProcessingLoggerObserver();
        observer1.Subscribe(publisher1);

        var input1 = "привіт світ";
        var processed1 = context1.Process(input1);

        LoggerManager.Instance.Log($"Main: Processed '{input1}' with '{context1.CurrentStrategyName}'");
        publisher1.PublishDataProcessed(processed1, context1.CurrentStrategyName);

        // =========================
        // СЦЕНАРІЙ 2: Динамічна зміна логера
        // =========================
        Console.WriteLine();
        Console.WriteLine("=== Scenario 2: Switch logger to FileLoggerFactory ===");

        // Перемикаємо фабрику в LoggerManager (тепер лог пишеться у файл)
        LoggerManager.Initialize(new FileLoggerFactory("log.txt"));

        var input2 = "привіт світ";
        var processed2 = context1.Process(input2);

        LoggerManager.Instance.Log($"Main: Processed '{input2}' with '{context1.CurrentStrategyName}'");
        publisher1.PublishDataProcessed(processed2, context1.CurrentStrategyName);

        Console.WriteLine("Перевір файл 'log.txt' — там мають бути логи Scenario 2 (і далі).");

        // =========================
        // СЦЕНАРІЙ 3: Динамічна зміна стратегії
        // =========================
        Console.WriteLine();
        Console.WriteLine("=== Scenario 3: Switch strategy to CompressDataStrategy ===");

        context1.SetStrategy(new CompressDataStrategy());

        var input3 = "привіт світ";
        var processed3 = context1.Process(input3);

        LoggerManager.Instance.Log($"Main: Processed '{input3}' with '{context1.CurrentStrategyName}'");
        publisher1.PublishDataProcessed(processed3, context1.CurrentStrategyName);

        Console.WriteLine("Scenario 3 теж залогується у 'log.txt', бо логер уже перемкнули у Scenario 2.");
    }
}
