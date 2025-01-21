using System;
using System.Collections.Generic;
using System.Linq;

namespace Translation_of_fighters
{
    public class Programm
    {
        private static void Main()
        {
            MilitaryBase militaryBase = new MilitaryBase();
            militaryBase.ShowSquads();
            militaryBase.TransferSolders();
            militaryBase.ShowSquads();
        }
    }

    public class MilitaryBase
    {
        private List<Solder> _firstSquads;
        private List<Solder> _secondSquads;

        public MilitaryBase()
        {
            _firstSquads = CreateSolders();
            _secondSquads = CreateSolders();
        }

        public void ShowSquads()
        {
            ShowSquad("Первый отряд:", _firstSquads);
            ShowSquad("Второй отряд:", _secondSquads);
        }

        public void TransferSolders()
        {
            char symbol = 'Б';
            List<Solder> tempSolders = _firstSquads.Where(solder => solder.Name.StartsWith(symbol.ToString())).ToList();
            _secondSquads = _secondSquads.Union(tempSolders).ToList();
            _firstSquads = _firstSquads.Except(tempSolders).ToList();
        }

        private void ShowSquad(string squadName, List<Solder> solders)
        {
            Console.WriteLine(squadName);
            solders.ForEach(solder => Console.WriteLine(solder.Name));
        }

        private List<Solder> CreateSolders()
        {
            char space = ' ';
            int soldersCount = 10;
            List<Solder> solders = new List<Solder>();

            for (int i = 0; i < soldersCount; i++)
            {
                string fullName = GetRandomSurnames();
                fullName += space + GetInitials();
                solders.Add(new Solder(fullName));
            }

            return solders;
        }

        private string GetRandomSurnames()
        {
            List<string> surnames = new List<string>
            {
                "Иванов",
                "Быстров",
                "Петров",
                "Белов",
                "Семенов",
                "Белкин",
                "Попов",
                "Смирнов",
                "Беглов",
                "Кузнецов",
                "Борзов",
                "Соколов"
            };

            return surnames[UserUtils.GenerateRandomNumber(surnames.Count)];
        }

        private string GetInitials()
        {
            char dot = '.';
            char[] unaccepttableSymbols = new char[] { 'Ъ', 'Ь' };
            string value = string.Empty;
            int initialsLength = 4;

            while (value.Length < initialsLength)
            {
                char first = GetRandomSymbol();

                if (unaccepttableSymbols.Contains(first) == false)
                {
                    value += $"{first}{dot}";
                }
            }

            return value;
        }

        private char GetRandomSymbol()
        {
            int min = 1040;
            int max = 1072;

            return Convert.ToChar(UserUtils.GenerateRandomNumber(min, max));
        }
    }

    public class Solder
    {
        public Solder(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public static class UserUtils
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
