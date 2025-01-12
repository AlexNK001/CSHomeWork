using System;
using System.Collections.Generic;
using System.Threading;

namespace _10_War
{
    public partial class DisplayBattlefield
    {
        private const int CellHeight = 2;
        private const int ThreadSleep = 10;

        private readonly int _cellWidth;
        private readonly Dictionary<Solder, WarriorViewPosition> _solders;
        private readonly Dictionary<SolderStatus, Config> _configs;
        private readonly Platoon _firstPlatoon;
        private readonly Platoon _secondPlatoon;

        public DisplayBattlefield(
            Platoon firstPlatoon,
            Platoon secondPlatoon,
            int cellWidth = 30,
            int indentationByWidth = 1)
        {
            _firstPlatoon = firstPlatoon;
            _secondPlatoon = secondPlatoon;
            _cellWidth = cellWidth;
            _solders = new Dictionary<Solder, WarriorViewPosition>();
            AddSolders(_firstPlatoon, indentationByWidth);
            int left = _cellWidth + indentationByWidth + indentationByWidth;
            AddSolders(_secondPlatoon, left);
            _configs = GetConfigs();
        }

        public void ShowBattleScreen()
        {
            Console.CursorVisible = false;

            foreach (Solder solder in _solders.Keys)
            {
                WarriorViewPosition viewPosition = _solders[solder];
                Console.SetCursorPosition(viewPosition.Left, viewPosition.Top);
                ShowSolderInfo(solder);
            }
        }

        public void ShowEndScreen()
        {
            int left = 0;
            int top = 0;
            string finalMessage;

            Console.Clear();
            Console.SetCursorPosition(left, top);

            if (_firstPlatoon.IsAlive == false && _secondPlatoon.IsAlive == false)
            {
                finalMessage = "Оба отряда мертвы. Ничья.";
            }
            else if (_firstPlatoon.IsAlive == false)
            {
                finalMessage = "Первый отряд пал. Второй победил!";
            }
            else
            {
                finalMessage = "Второй отряд пал. Первый победил!";
            }

            Console.WriteLine(finalMessage);

            foreach (Solder solder in _solders.Keys)
            {
                solder.Attacked -= OnUpdateAttackingSolderInfo;
                solder.ReceivedDamage -= OnUpdateAttackedSolderInfo;
            }
        }

        private void AddSolders(Platoon platoon, int left, int shiftDown = 1)
        {
            IReadOnlyList<Solder> solders = platoon.GetSolders();

            for (int i = 0; i < solders.Count; i++)
            {
                Solder currentSolder = solders[i];
                currentSolder.Attacked += OnUpdateAttackingSolderInfo;
                currentSolder.ReceivedDamage += OnUpdateAttackedSolderInfo;
                int top = i * (CellHeight + shiftDown);
                _solders.Add(currentSolder, new WarriorViewPosition(top, left));
            }
        }

        private void OnUpdateAttackingSolderInfo(Solder solder)
        {
            UpdateSolderInfo(solder, SolderStatus.Attacking);
        }

        private void OnUpdateAttackedSolderInfo(Solder solder)
        {
            UpdateSolderInfo(solder, SolderStatus.Attacked);
        }

        private void UpdateSolderInfo(Solder solder, SolderStatus status)
        {
            ShowSolderInfo(solder, status);
            Thread.Sleep(ThreadSleep);
            status = solder.IsALive ? SolderStatus.Alive : SolderStatus.Dead;
            ShowSolderInfo(solder, status);
        }

        private void ShowSolderInfo(Solder solder, SolderStatus status = SolderStatus.Alive)
        {
            Config config = _configs[status];
            WarriorViewPosition viewPosition = _solders[solder];

            string name = solder.Name + config.Status;
            name = AlignNameWidth(name);
            string bar = GetBar(solder.PercentageHealth());

            ChangeColor(config.Background, config.Foreground);

            WriteLine(name, viewPosition.Left, viewPosition.Top);
            viewPosition.Top++;
            WriteLine(bar, viewPosition.Left, viewPosition.Top);

            Console.ResetColor();
        }

        private void WriteLine(string text, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }

        private string AlignNameWidth(string name)
        {
            return name.Length < _cellWidth ? name.PadRight(_cellWidth) : name.Remove(_cellWidth);
        }

        private void ChangeColor(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }

        private string GetBar(float currentShare)
        {
            char startSymbol = '[';
            char presenceSymbol = '#';
            char absenceSymbol = '_';
            char endSymbol = ']';
            int numberDecorativeSymbols = 2;
            string bar = string.Empty;

            int currentBarSize = _cellWidth - numberDecorativeSymbols;
            int numberSymbolAvailable = Convert.ToInt32(currentBarSize * currentShare);

            for (int i = 0; i < currentBarSize; i++)
            {
                bar += i < numberSymbolAvailable ? presenceSymbol : absenceSymbol;
            }

            return $"{startSymbol}{bar}{endSymbol}";
        }

        private Dictionary<SolderStatus, Config> GetConfigs()
        {
            return new Dictionary<SolderStatus, Config>
            {
                { SolderStatus.Alive, new Config(ConsoleColor.DarkRed, ConsoleColor.Gray, " Живой") },
                { SolderStatus.Attacked, new Config(ConsoleColor.Black, ConsoleColor.Green, " Атакующий") },
                { SolderStatus.Attacking, new Config(ConsoleColor.Black, ConsoleColor.DarkYellow, " Атакуемый") },
                { SolderStatus.Dead, new Config(ConsoleColor.DarkGray, ConsoleColor.Gray, " Мертвый") }
            };
        }
    }
}
