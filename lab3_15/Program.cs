using System;
using System.Collections.Generic;

namespace RobotsApp
{
    // Базовий клас Robot
    class Robot
    {
        public string Name { get; set; }

        // Конструктор базового класу
        public Robot(string name)
        {
            Name = name;
        }

        // Віртуальний метод, який перевизначатимуть похідні класи
        public virtual void Work()
        {
            Console.WriteLine($"{Name} виконує загальну роботу.");
        }
    }

    // Похідний клас CleanerRobot
    class CleanerRobot : Robot
    {
        public CleanerRobot(string name) : base(name) // виклик конструктора базового класу
        {
        }

        // Перевизначення методу Work
        public override void Work()
        {
            Console.WriteLine($"{Name} прибирає приміщення.");
        }
    }

    // Похідний клас DeliveryRobot
    class DeliveryRobot : Robot
    {
        public DeliveryRobot(string name) : base(name)
        {
        }

        // Перевизначення методу Work
        public override void Work()
        {
            Console.WriteLine($"{Name} доставляє посилки.");
        }
    }

    // Демонстрація поліморфізму
    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо список роботів
            List<Robot> robots = new List<Robot>
            {
                new CleanerRobot("RoboCleaner-01"),
                new DeliveryRobot("DeliverBot-07"),
                new Robot("SimpleBot")
            };

            // Поліморфізм кожен робот виконує власну дію через один метод Work()
            foreach (var robot in robots)
            {
                robot.Work();
            }

            Console.ReadKey();
        }
    }
}

