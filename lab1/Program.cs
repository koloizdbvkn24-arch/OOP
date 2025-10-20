using System;

namespace FiguresApp
{
    // Абстрактний клас Figure
    abstract class Figure
    {
        private string _name; // приватне поле

        // Конструктор
        public Figure(string name)
        {
            _name = name;
        }

        // Деструктор
        ~Figure()
        {
            Console.WriteLine($"{_name} видалено з пам’яті.");
        }

        // Публічна властивість
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Віртуальний метод (для перевизначення)
        public virtual double GetArea()
        {
            return 0;
        }
    }

    class Circle : Figure
    {
        private double _radius;

        // Конструктор Circle — викликає базовий конструктор Figure
        public Circle(string name, double radius) : base(name)
        {
            _radius = radius;
        }

        // 5. Перевизначення методу GetArea
        public override double GetArea()
        {
            return Math.PI * _radius * _radius;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо об’єкт класу Circle
            Circle circle = new Circle("Коло", 5.0);

            // Виводимо площу
            Console.WriteLine($"Фігура: {circle.Name}");
            Console.WriteLine($"Площа кола: {circle.GetArea():F2}");

            Console.ReadKey();
        }
    }
}
