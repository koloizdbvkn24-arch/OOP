namespace StrategyObserverDemo
{

    public interface INumericOperationStrategy
    {
        double Execute(double value);
        string Name { get; }   // щоб зручно передавати назву операції в Observer
    }

    public sealed class SquareOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Square";
        public double Execute(double value) => value * value;
    }

    public sealed class CubeOperationStrategy : INumericOperationStrategy
    {
        public string Name => "Cube";
        public double Execute(double value) => value * value * value;
    }

    public sealed class SquareRootOperationStrategy : INumericOperationStrategy
    {
        public string Name => "SquareRoot";

        public double Execute(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot take square root of a negative number.");

            return Math.Sqrt(value);
        }
    }

    public sealed class NumericProcessor
    {
        private INumericOperationStrategy _strategy;

        public NumericProcessor(INumericOperationStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void SetStrategy(INumericOperationStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public double Process(double input) => _strategy.Execute(input);

        public string CurrentOperationName => _strategy.Name;
    }


    public sealed class ResultPublisher
    {
        public event Action<double, string>? ResultCalculated;

        public void PublishResult(double result, string operationName)
        {
            ResultCalculated?.Invoke(result, operationName);
        }
    }

    public sealed class ConsoleLoggerObserver
    {
        public void Subscribe(ResultPublisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            publisher.ResultCalculated += OnResultCalculated;
        }

        public void Unsubscribe(ResultPublisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            publisher.ResultCalculated -= OnResultCalculated;
        }

        private void OnResultCalculated(double result, string operationName)
        {
            Console.WriteLine($"[ConsoleLogger] Operation: {operationName}, Result: {result}");
        }
    }

    public sealed class HistoryLoggerObserver
    {
        public List<string> History { get; } = new();

        public void Subscribe(ResultPublisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            publisher.ResultCalculated += OnResultCalculated;
        }

        public void Unsubscribe(ResultPublisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            publisher.ResultCalculated -= OnResultCalculated;
        }

        private void OnResultCalculated(double result, string operationName)
        {
            History.Add($"Operation: {operationName}, Result: {result}");
        }
    }

    public sealed class ThresholdNotifierObserver
    {
        private readonly double _threshold;

        public ThresholdNotifierObserver(double threshold)
        {
            _threshold = threshold;
        }

        public void Subscribe(ResultPublisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            publisher.ResultCalculated += OnResultCalculated;
        }

        public void Unsubscribe(ResultPublisher publisher)
        {
            if (publisher == null) throw new ArgumentNullException(nameof(publisher));
            publisher.ResultCalculated -= OnResultCalculated;
        }

        private void OnResultCalculated(double result, string operationName)
        {
            if (result > _threshold)
            {
                Console.WriteLine($"[ThresholdNotifier] ALERT: {operationName} result {result} > threshold {_threshold}");
            }
        }
    }


    internal static class Program
    {
        private static void Main()
        {
            var publisher = new ResultPublisher();

            var consoleObserver = new ConsoleLoggerObserver();
            var historyObserver = new HistoryLoggerObserver();
            var thresholdObserver = new ThresholdNotifierObserver(threshold: 50);

            consoleObserver.Subscribe(publisher);
            historyObserver.Subscribe(publisher);
            thresholdObserver.Subscribe(publisher);

            var processor = new NumericProcessor(new SquareOperationStrategy());

            double[] inputs = { 2, 3, 8, 16 };

            // 1) Square
            foreach (var x in inputs)
            {
                var result = processor.Process(x);
                publisher.PublishResult(result, processor.CurrentOperationName);
            }

            Console.WriteLine();

            // 2) Cube
            processor.SetStrategy(new CubeOperationStrategy());
            foreach (var x in inputs)
            {
                var result = processor.Process(x);
                publisher.PublishResult(result, processor.CurrentOperationName);
            }

            Console.WriteLine();

            // 3) SquareRoot
            processor.SetStrategy(new SquareRootOperationStrategy());
            foreach (var x in inputs)
            {
                var result = processor.Process(x);
                publisher.PublishResult(result, processor.CurrentOperationName);
            }

            Console.WriteLine("\n--- HISTORY ---");
            foreach (var entry in historyObserver.History)
                Console.WriteLine(entry);
        }
    }
}
