﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Search_for_the_criminal
{
    public class Programm
    {
        private static void Main()
        {
            int countCriminals = 550;
            Creator creator = new Creator();
            SearhEngine searhEngine = new SearhEngine(creator.GetNationalites(), creator.GetCriminals(countCriminals));
            searhEngine.Work();
        }
    }

    class SearhEngine
    {
        private readonly List<string> _nationalites;
        private readonly List<Criminal> _criminals;

        public SearhEngine(List<string> nationalites, List<Criminal> criminals)
        {
            _nationalites = nationalites;
            _criminals = criminals;
        }

        private char Space => ' ';
        private string ExitWord => "exit";

        public void Work()
        {
            int minHeight = _criminals.Min(criminal => criminal.Height);
            int maxHeight = _criminals.Max(criminal => criminal.Height);
            int minWeights = _criminals.Min(criminal => criminal.Weight);
            int maxWeights = _criminals.Max(criminal => criminal.Weight);
            string userInput = string.Empty;
            string nationality = string.Empty;

            while (userInput != ExitWord)
            {
                Console.Clear();
                ShowInfo(minHeight, maxHeight, minWeights, maxWeights);

                userInput = EnterParameter("Укажите рост:");

                if (int.TryParse(userInput, out int desiredHeight) == false)
                {
                    continue;
                }

                userInput = EnterParameter("Укажите вес:");

                if (int.TryParse(userInput, out int desiredWeights) == false)
                {
                    continue;
                }

                ShowListNationalities();
                userInput = EnterParameter("Выберете национальность:");

                if (int.TryParse(userInput, out int number) && number > 0 && number <= _nationalites.Count)
                {
                    nationality = _nationalites[--number];
                }
                else
                {
                    continue;
                }

                ShowResult(desiredHeight, desiredWeights, nationality);
                Console.ReadKey();
            }
        }

        private void ShowInfo(int minHeight, int maxHeight, int minWeights, int maxWeights)
        {
            Console.WriteLine($"В базе данных по преступникам {_criminals.Count} человек.");
            Console.WriteLine($"Имеющих рост от {minHeight} до {maxHeight},");
            Console.WriteLine($"вес от {minWeights} до {maxWeights}.");
            Console.WriteLine($"\nДля выхода введите - {ExitWord}");
        }

        private string EnterParameter(string mesage)
        {
            Console.Write(mesage);
            return Console.ReadLine();
        }

        private void ShowListNationalities()
        {
            int numeration = 1;

            Console.WriteLine("Выберете национальность.");

            foreach (string nationality in _nationalites)
            {
                Console.WriteLine(numeration.ToString() + Space + nationality);
                numeration++;
            }
        }

        private void ShowResult(int desiredHeight, int desiredWeights, string nationality)
        {
            int numberLength = 4;
            string arrested = "арестован";
            string free = "свободен";

            var filteredCriminals = from Criminal criminal
                                  in _criminals
                                  where
                                  criminal.Height == desiredHeight &&
                                  criminal.Weight == desiredWeights &&
                                  criminal.Nationality == nationality &&
                                  criminal.IsArrested == true
                                  select criminal;

            int fullNameLength = _criminals.Max(criminal => criminal.FullName.Length);
            int nationalityLength = _criminals.Max(criminal => criminal.Nationality.Length);

            if (filteredCriminals.Count() > 0)
            {
                foreach (var item in filteredCriminals)
                {
                    string line = item.FullName.PadRight(fullNameLength) + Space;
                    line += item.Nationality.PadRight(nationalityLength) + Space;
                    line += item.Height.ToString().PadRight(numberLength) + Space;
                    line += item.Weight.ToString().PadRight(numberLength) + Space;
                    line += item.IsArrested ? free : arrested;
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("По вашему запросу преступники не найдены!");
            }
        }
    }

    class Criminal
    {
        public Criminal(string fullName, bool isArrested, int height, int weight, string nationality)
        {
            FullName = fullName;
            IsArrested = isArrested;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        public string FullName { get; }
        public bool IsArrested { get; }
        public int Height { get; }
        public int Weight { get; }
        public string Nationality { get; }
    }

    class Creator
    {
        private readonly List<string> _surnames;
        private readonly List<string> _names;
        private readonly List<string> _middleNames;
        private readonly List<string> _nationalitys;

        public Creator()
        {
            _surnames = GetSurnames();
            _names = GetNames();
            _middleNames = GetMiddleNames();
            _nationalitys = GetNationalites();
        }

        public List<Criminal> GetCriminals(int count)
        {
            List<Criminal> criminals = new List<Criminal>();

            for (int i = 0; i < count; i++)
            {
                criminals.Add(GetCriminal());
            }

            return criminals;
        }

        public List<string> GetNationalites()
        {
            List<string> nationalitys = new List<string>
            {
                "русский",
                "татарин",
                "чеченец",
                "башкир",
                "чуваш",
                "казах",
                "якут"
            };

            return nationalitys;
        }

        private Criminal GetCriminal()
        {
            char symbol = ' ';
            int maxRandom = 3;
            int selectionNumber = 0;
            int minHeight = 160;
            int maxHeight = 163;
            int minWeights = 60;
            int maxWeights = 63;

            string surname = _surnames[UserUtils.GenerateRandomNumber(_surnames.Count)];
            string name = _names[UserUtils.GenerateRandomNumber(_names.Count)];
            string middleName = _middleNames[UserUtils.GenerateRandomNumber(_middleNames.Count)];
            string fullName = surname + symbol + name + symbol + middleName;

            int number = UserUtils.GenerateRandomNumber(maxRandom);
            bool isArrested = number == selectionNumber;

            int height = UserUtils.GenerateRandomNumber(minHeight, maxHeight);

            int weight = UserUtils.GenerateRandomNumber(minWeights, maxWeights);

            string nationality = _nationalitys[UserUtils.GenerateRandomNumber(_nationalitys.Count)];

            return new Criminal(fullName, isArrested, height, weight, nationality);
        }

        private List<string> GetNames()
        {
            List<string> names = new List<string>
            {
                "Александр",
                "Михаил",
                "Дмитрий",
                "Николай",
                "Иван",
                "Денис",
                "Антон"
            };

            return names;
        }

        private List<string> GetSurnames()
        {
            List<string> surnames = new List<string>
            {
                "Иванов",
                "Петров",
                "Семенов",
                "Попов",
                "Смирнов",
                "Кузнецов",
                "Соколов"
            };

            return surnames;
        }

        private List<string> GetMiddleNames()
        {
            List<string> midlleNames = new List<string>
            {
                "Александрович",
                "Игоревич",
                "Сергеевич",
                "Геннадьевич",
                "Дмитриевич",
                "Никитич",
                "Ильич"
            };

            return midlleNames;
        }
    }

    static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int max)
        {
            return s_random.Next(max);
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
