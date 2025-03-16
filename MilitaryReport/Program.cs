using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryReport
{
    internal class Program
    {
        private static void Main()
        {
            ProgramRunner.Run();
        }
    }

    class ProgramRunner
    {
        public static void Run()
        {
            Database database = new Database();

            Console.WriteLine("Полный список солдат:");
            database.ShowAllSoldiers();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Показать Имя и Звание");
                Console.WriteLine("2 - Показать Имя и Вооружение");
                Console.WriteLine("3 - Выход");

                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Отчёт (Имя и Звание):");
                        database.ShowReport(soldier => (soldier.Name, soldier.Rank), "Звание");
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Отчёт (Имя и Вооружение):");
                        database.ShowReport(soldier => (soldier.Name, soldier.Weapon), "Вооружение");
                        break;

                    case "3":
                        isRunning = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Неверная команда. Попробуйте снова.");
                        break;
                }
            }
        }
    }

    class Database
    {
        private readonly List<Soldier> _soldiers;

        public Database()
        {
            _soldiers = GenerateSoldiers();
        }

        public void ShowAllSoldiers()
        {
            foreach (var soldier in _soldiers)
            {
                Console.WriteLine($"Имя: {soldier.Name}, Звание: {soldier.Rank}, Вооружение: {soldier.Weapon}, Срок службы: {soldier.ServiceTerm} мес.");
            }
        }

        public void ShowReport(Func<Soldier, (string Name, string Value)> selector, string parameterName)
        {
            var report = _soldiers.Select(selector);

            foreach (var item in report)
            {
                Console.WriteLine($"Имя: {item.Name}, {parameterName}: {item.Value}");
            }
        }

        private static List<Soldier> GenerateSoldiers()
        {
            return new List<Soldier>
            {
                new Soldier("Иван Петров", "Автомат АК-74", "Рядовой", 12),
                new Soldier("Алексей Смирнов", "Снайперская винтовка СВД", "Сержант", 24),
                new Soldier("Дмитрий Козлов", "Пистолет Макарова", "Лейтенант", 36),
                new Soldier("Сергей Васильев", "Пулемёт ПКМ", "Капитан", 48),
                new Soldier("Николай Сидоров", "Гранатомёт РПГ-7", "Майор", 60)
            };
        }
    }

    class Soldier
    {
        public Soldier(string name, string weapon, string rank, int serviceTerm)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            ServiceTerm = serviceTerm;
        }

        public string Name { get; }
        public string Weapon { get; }
        public string Rank { get; }
        public int ServiceTerm { get; }
    }
}


