using System;
using System.Collections.Generic;

namespace _3_Player_database
{
    public class Program
    {
        private static void Main()
        {
            const string CommandAddPlayer = "1";
            const string CommandBan = "2";
            const string CommandUnban = "3";
            const string CommandDelete = "4";
            const string CommandExit = "5";

            bool isProgramm = true;

            Database players = new Database();

            while (isProgramm)
            {
                Console.WriteLine($"{CommandAddPlayer} - Добавить нового игрока.");
                Console.WriteLine($"{CommandBan} - Забанить игрока.");
                Console.WriteLine($"{CommandUnban} - Разбанить игрока.");
                Console.WriteLine($"{CommandDelete} - Удалить игрока.");
                Console.WriteLine($"{CommandExit} - Выход.");

                players.ShowPlayers();

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddPlayer:
                        players.AddPlayer();
                        break;

                    case CommandBan:
                        players.BanPlayer();
                        break;

                    case CommandUnban:
                        players.UnbanPlayer();
                        break;

                    case CommandDelete:
                        players.DeletePlayer();
                        break;

                    case CommandExit:
                        isProgramm = false;
                        break;
                }

                Console.Clear();
            }
        }
    }

    public class Player
    {
        private int _identifications;
        private string _name;
        private int _level;

        public Player(string name)
        {
            Identification = ++_identifications;
            IsBanned = false;
            _name = name;
            _level = 0;
        }

        public bool IsBanned { get; private set; }
        public int Identification { get; private set; }

        public void ShowStats()
        {
            string name = _name;
            string identification = Identification.ToString();
            string level = _level.ToString();
            string isBaned = IsBanned.ToString();

            WriteColorLine($"{ShowStat(name, 10)}" +
                $"{ShowStat(identification, 3)}" +
                $"{ShowStat(level, 3)}" +
                $"{ShowStat(isBaned, 6)}");

            string ShowStat(string stat, int indent)
            {
                return $"{stat}{new string(' ', indent - stat.Length)}|";
            }
        }

        public void Ban()
        {
            if (IsBanned)
                WriteColorLine("Игрок уже забанен.", true);
            else
                IsBanned = true;
        }

        public void Unban()
        {
            if (IsBanned)
                IsBanned = false;
            else
                WriteColorLine("Игрок еще не забанен.", true);
        }

        private void WriteColorLine(string text, bool isPause = false)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();

            if (isPause)
                Console.ReadKey();
        }
    }

    public class Database
    {
        private List<Player> _players;

        public Database()
        {
            _players = new List<Player>();
        }

        public void ShowPlayers()
        {
            if (_players.Count > 0)
            {
                WriteColorLine($"NAME{new string(' ', 6)}|ID |LVL|BANED |");

                foreach (Player player in _players)
                    player.ShowStats();
            }
            else
            {
                WriteColorLine("Список игроков пуст.");
            }
        }

        public void AddPlayer()
        {
            Console.Clear();
            WriteColorLine("Введите имя игрока:");
            string userInput = Console.ReadLine();

            if (userInput == "")
                WriteColorLine("Имя игрока должно содержать символы.", true);
            else
                _players.Add(new Player($"{userInput}"));
        }

        public void BanPlayer()
        {
            Console.Clear();

            if (_players.Count > 0)
            {
                int countPlayers = 0;

                WriteColorLine($"NAME{new string(' ', 6)}|ID |LVL|BANED |");

                foreach (Player player in _players)
                {
                    if (player.IsBanned == false)
                    {
                        player.ShowStats();
                        ++countPlayers;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (countPlayers > 0)
                {
                    WriteColorLine("Введите ID игрока, которого хотите забанить.");

                    if (TryFindePlayer(out Player player))
                        player.Ban();
                    else
                        WriteColorLine("Игрок с таким ID не найден", true);
                }
                else
                {
                    Console.Clear();
                    WriteColorLine("Забаненных игроков нет.", true);
                }
            }
            else
            {
                WriteColorLine("Список игроков пуст.", true);
            }
        }

        public void UnbanPlayer()
        {
            Console.Clear();

            if (_players.Count > 0)
            {
                int countPlayers = 0;

                WriteColorLine($"NAME{new string(' ', 6)}|ID |LVL|BANED |");

                foreach (Player player in _players)
                {
                    if (player.IsBanned == true)
                    {
                        player.ShowStats();
                        ++countPlayers;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (countPlayers > 0)
                {
                    WriteColorLine("Введите ID игрока, которого хотите разбанить.");

                    if (TryFindePlayer(out Player player))
                        player.Unban();
                    else
                        WriteColorLine("Игрок с таким ID не найден", true);
                }
                else
                {
                    Console.Clear();
                    WriteColorLine("Забаненых игроков нет.", true);
                }
            }
            else
            {
                WriteColorLine("Список игроков пуст.", true);
            }
        }

        public void DeletePlayer()
        {
            Console.Clear();

            if (_players.Count > 0)
            {
                ShowPlayers();

                WriteColorLine("Введите ID игрока, которого хотите удалить.");

                if (TryFindePlayer(out Player player))
                    _players.Remove(player);
                else
                    WriteColorLine("Игрок с таким ID не найден", true);
            }
            else
            {
                WriteColorLine("Список игроков пуст.", true);
            }
        }

        private void WriteColorLine(string text, bool isPause = false)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(text);
            Console.ResetColor();

            if (isPause)
                Console.ReadKey();
        }

        private bool TryFindePlayer(out Player player)
        {
            player = null;
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int result))
            {
                for (int i = 0; i < _players.Count; i++)
                {
                    if (_players[i].Identification == result)
                        player = _players[i];
                }
            }

            return result >= 0 && player != null;
        }
    }
}
