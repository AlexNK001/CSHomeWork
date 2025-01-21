using System;
using System.Collections.Generic;
using System.Linq;

namespace Top_players_of_the_server
{
    public class Programm
    {
        private static void Main()
        {
            GameServer gameServer = new GameServer();
            gameServer.ShowTopPlayers();
        }
    }

    public class GameServer
    {
        private const string CommandShowByLevel = "1";
        private const string CommandShowByStrength = "2";
        private const string CommandExit = "3";
        private const int NumberInTopPlayers = 3;

        private readonly List<Player> _players;

        public GameServer()
        {
            _players = GetPlayers();
        }

        public void ShowTopPlayers()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{CommandShowByLevel} - Показать топ {NumberInTopPlayers} игроков по уровню.");
                Console.WriteLine($"{CommandShowByStrength} - Показать топ {NumberInTopPlayers} игроков по силе.");
                Console.WriteLine($"{CommandExit} - Выход.");

                switch (Console.ReadLine())
                {
                    case CommandShowByLevel:
                        ShowTopPlayersByLevel();
                        break;

                    case CommandShowByStrength:
                        ShowTopPlayersByStrength();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowTopPlayersByLevel()
        {
            var topHighLevelPlayers = _players.OrderBy(player => player.Level).Take(NumberInTopPlayers);
            Console.WriteLine($"Показан топ {NumberInTopPlayers} по уровню.");
            ShowTopPlayers(topHighLevelPlayers);
        }

        private void ShowTopPlayersByStrength()
        {
            var topStrongestPlayers = _players.OrderBy(player => player.Strength).Take(NumberInTopPlayers);
            Console.WriteLine($"Показан топ {NumberInTopPlayers} по силе.");
            ShowTopPlayers(topStrongestPlayers);
        }

        private void ShowTopPlayers(IEnumerable<Player> sortedPlayers)
        {
            char symbol = '|';
            int numeration = 1;
            int numberLength = 1;
            string name = "Имя игрока:";
            string level = "Уровень:";
            string strength = "Сила:";

            int MaxNameLength = _players.Max(player => player.Name.Length);
            MaxNameLength = MaxNameLength > name.Length ? MaxNameLength : name.Length;

            string line = new string(' ', numberLength) + symbol;
            line += name + symbol + level + symbol + strength + symbol;
            Console.WriteLine(line);

            foreach (var player in sortedPlayers)
            {
                line = numeration.ToString().PadRight(numberLength) + symbol;
                line += player.Name.PadRight(MaxNameLength) + symbol;
                line += player.Level.ToString().PadRight(level.Length) + symbol;
                line += player.Strength.ToString().PadRight(strength.Length) + symbol;
                Console.WriteLine(line);

                numeration++;
            }
        }

        private List<Player> GetPlayers()
        {
            List<Player> players = new List<Player>
            {
                new Player("Мимас", 45, 65),
                new Player("Энцелад", 78, 45),
                new Player("Тефия", 62, 36),
                new Player("Диона", 91, 78),
                new Player("Рея", 35, 95),
                new Player("Титан", 16, 23),
                new Player("Гиперион", 94, 45),
                new Player("Япет", 35, 65),
                new Player("Феба", 79, 87),
                new Player("Янус", 23, 32),
                new Player("Эпиметей", 48, 49)
            };

            return players;
        }
    }

    public class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; }
        public int Level { get; }
        public int Strength { get; }
    }
}
