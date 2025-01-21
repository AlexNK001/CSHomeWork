using System;
using System.Collections.Generic;
using System.Linq;

namespace Definition_of_overdue
{
    public class Programm
    {
        private static void Main()
        {
            int srewCount = 30;
            MeatProcessingPlant meatProcessingPlant = new MeatProcessingPlant();
            FoodWarehouse foodWarehouse = new FoodWarehouse(meatProcessingPlant.GetSrews(srewCount));
            foodWarehouse.ShowExpiredStew();
        }
    }

    public class FoodWarehouse
    {
        private readonly List<Srew> _srews;

        public FoodWarehouse(List<Srew> srews)
        {
            _srews = srews;
        }

        public void ShowExpiredStew()
        {
            int maxNameLength = _srews.Max(srew => srew.Name.Length);
            int numeration = 1;
            string line = string.Empty;
            string exitWord = "exit";
            string userInput = string.Empty;

            while (userInput != exitWord)
            {
                Console.WriteLine("Укажите дату, на момент которой вы хотите увидеть просроченный продукт:");
                Console.WriteLine($"Для выхода введите {exitWord}.");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int currentData))
                {
                    var selected = _srews.Where(p => p.YearOfProduction + p.ExpirationDate < currentData);

                    Console.WriteLine(_srews.Count);

                    foreach (var item in selected)
                    {
                        Console.WriteLine($"{numeration}){item.Name.PadRight(maxNameLength)} {item.YearOfProduction} {item.ExpirationDate}");
                        numeration++;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод.");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    public class MeatProcessingPlant
    {
        public List<Srew> GetSrews(int count)
        {
            List<Srew> srews = new List<Srew>();

            string name = "Тушенка";
            int minYearOfProduction = 1992;
            int maxYearOfProduction = 2020;
            int minExpirationDate = 5;
            int maxExpirationDate = 15;

            for (int i = 0; i < count; i++)
            {
                int yearOfProduction = UserUtils.GenerateRandomNumber(minYearOfProduction, maxYearOfProduction);
                int expirationDate = UserUtils.GenerateRandomNumber(minExpirationDate, maxExpirationDate);
                srews.Add(new Srew(name, yearOfProduction, expirationDate));
            }

            return srews;
        }
    }

    public class Srew
    {
        public Srew(string name, int yearOfProduction, int expirationDate)
        {
            Name = name;
            YearOfProduction = yearOfProduction;
            ExpirationDate = expirationDate;
        }

        public string Name { get; }
        public int YearOfProduction { get; }
        public int ExpirationDate { get; }
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
