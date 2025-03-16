using System;
using System.Collections.Generic;
using System.Linq;

namespace MilitaryReport
{
    internal class Program
    {
        private static void Main()
        {
            var databaseHandler = new DatabaseHandler();
            databaseHandler.Run();
        }
    }

    class DatabaseHandler
    {
        private const string ShowRankCommand = "1";
        private const string ShowWeaponCommand = "2";
        private const string ExitCommand = "3";

        private readonly Database _database;

        public DatabaseHandler()
        {
            _database = new Database();
        }

        public void Run()
        {
            Console.WriteLine("Полный список солдат:");
            _database.ShowAllSoldiers();

            bool isRunning = true;

            while (isRunning)
            {
                ShowMenu();
                string command = Console.ReadLine();

                switch (command)
                {
                    case ShowRankCommand:
                        ShowReport("Звание", soldier => soldier.Rank);
                        break;

                    case ShowWeaponCommand:
                        ShowReport("Вооружение", soldier => soldier.Weapon);
                        break;

                    case ExitCommand:
                        isRunning = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Неверная команда. Попробуйте снова.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine($"{ShowRankCommand} - Показать Имя и Звание");
            Console.WriteLine($"{ShowWeaponCommand} - Показать Имя и Вооружение");
            Console.WriteLine($"{ExitCommand} - Выход");
            Console.Write("Введите команду: ");
        }

        private void ShowReport(string parameterName, Func<Soldier, string> selector)
        {
            Console.Clear();
            Console.WriteLine($"Отчёт ({parameterName}):");

            var report = _database.GetReport(selector);
            report.ForEach(Console.WriteLine);
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
            _soldiers.ForEach(soldier => Console.WriteLine(
                $"Имя: {soldier.Name}, Звание: {soldier.Rank}, Вооружение: {soldier.Weapon}, Срок службы: {soldier.ServiceTerm} мес."));
        }

        public List<string> GetReport(Func<Soldier, string> selector)
        {
            return _soldiers
                .Select(soldier => $"Имя: {soldier.Name}, {selector(soldier)}")
                .ToList();
        }

        private List<Soldier> GenerateSoldiers()
        {
            string[] names = { "Иван Петров", "Алексей Смирнов", "Дмитрий Козлов", "Сергей Васильев", "Николай Сидоров" };
            string[] weapons = { "Автомат АК-74", "Снайперская винтовка СВД", "Пистолет Макарова", "Пулемёт ПКМ", "Гранатомёт РПГ-7" };
            string[] ranks = { "Рядовой", "Сержант", "Лейтенант", "Капитан", "Майор" };

            return names
                .Select((name, index) => new Soldier(name, weapons[index], ranks[index], (index + 1) * 12))
                .ToList();
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