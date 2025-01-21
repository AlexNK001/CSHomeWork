using System;
using System.Collections.Generic;
using System.Linq;

namespace Weapons_Report
{
    public class Programm
    {
        private static void Main()
        {
            int soldersCount = 5;
            MilitaryAcademy militaryAcademy = new MilitaryAcademy();
            Squad squad = new Squad(militaryAcademy.GetSolders(soldersCount));
            squad.ShowIncompleteInfo();
        }
    }

    public class Squad
    {
        private readonly List<Solder> _solders;

        public Squad(List<Solder> solders)
        {
            _solders = solders;
        }

        public void ShowIncompleteInfo()
        {
            var incomleteInfo = _solders.Select(solder => new { solder.Name, solder.Rank });

            foreach (var solder in incomleteInfo)
            {
                Console.WriteLine($"{solder.Name} {solder.Rank}");
            }
        }
    }

    public class Solder
    {
        public Solder(string name, string weapons, string rank, int militaryService)
        {
            Name = name;
            Weapons = weapons;
            Rank = rank;
            MilitaryService = militaryService;
        }

        public string Name { get; }
        public string Weapons { get; }
        public string Rank { get; }
        public int MilitaryService { get; }
    }

    public class MilitaryAcademy
    {
        private readonly List<string> _names;
        private readonly List<string> _weapons;
        private readonly List<string> _rank;

        public MilitaryAcademy()
        {
            _names = GetNames();
            _weapons = GetWeapons();
            _rank = GetRanks();
        }

        public List<Solder> GetSolders(int count)
        {
            List<Solder> solder = new List<Solder>();
            int minMilitaryService = 5;
            int maxMilitaryService = 15;

            for (int i = 0; i < count; i++)
            {
                string name = _names[UserUtils.GenerateRandomNumber(_names.Count)];
                string weapons = _weapons[UserUtils.GenerateRandomNumber(_weapons.Count)];
                string rank = _rank[UserUtils.GenerateRandomNumber(_rank.Count)];
                solder.Add(new Solder(name, weapons, rank, UserUtils.GenerateRandomNumber(minMilitaryService, maxMilitaryService)));
            }

            return solder;
        }

        private List<string> GetNames()
        {
            List<string> names = new List<string>
            {
                "Alexander",
                "Michael",
                "Dmitriy",
                "Nikolai",
                "Ivan",
                "Denis",
                "Anton"
            };

            return names;
        }

        private List<string> GetWeapons()
        {
            return new List<string> { "Rifle", "Machiine Gun", "Mortar" };
        }

        private List<string> GetRanks()
        {
            return new List<string> { "Private", "Sergeant", "Officer" };
        }
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
