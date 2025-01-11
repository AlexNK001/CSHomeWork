using System;
using System.Collections.Generic;
using System.Threading;

namespace _10_War
{
    public partial class TextStorage
    {
        private const int CellHeight = 2;
        private const int ThreadSleep = 10;

        private readonly int _cellWidth;
        private readonly Dictionary<Warrior, SolderViewPosition> _solders;

        public TextStorage(Platoon firstPlatoon, Platoon secondPlatoon, int cellWidth = 30, int indentationByWidth = 1)
        {
            _cellWidth = cellWidth;
            _solders = new Dictionary<Warrior, SolderViewPosition>();
            AddSolders(firstPlatoon, indentationByWidth);
            int left = _cellWidth + indentationByWidth + indentationByWidth;
            AddSolders(secondPlatoon, left);
        }

        private void AddSolders(Platoon platoon, int left, int shiftDown = 1)
        {
            IReadOnlyList<Warrior> solders = platoon.GetSolders();

            for (int i = 0; i < solders.Count; i++)
            {
                Warrior currentSolder = solders[i];
                currentSolder.Attacked += OnUpdateAttackingSolderInfo;
                currentSolder.ReceivedDamage += OnUpdateAttackedSolderInfo;
                int top = i * (CellHeight + shiftDown);
                _solders.Add(currentSolder, new SolderViewPosition(top, left));
            }
        }

        private void OnUpdateAttackingSolderInfo(Warrior solder)
        {
            UpdateSolderInfo(solder, SolderStatus.Attacking);
        }

        private void OnUpdateAttackedSolderInfo(Warrior solder)
        {
            UpdateSolderInfo(solder, SolderStatus.Attacked);
        }

        private void UpdateSolderInfo(Warrior solder, SolderStatus status)
        {
            ShowSolderInfo(solder, status);
            Thread.Sleep(ThreadSleep);
            status = solder.IsALive ? SolderStatus.None : SolderStatus.Dead;
            ShowSolderInfo(solder, status);
        }

        public void ShowBattleScreen()
        {
            foreach (Warrior solder in _solders.Keys)
            {
                SolderViewPosition viewPosition = _solders[solder];
                Console.SetCursorPosition(viewPosition.Left, viewPosition.Top);
                ShowSolderInfo(solder);
            }
        }

        public void ShowSolderInfo(Warrior solder, SolderStatus status = SolderStatus.None)
        {
            string name = solder.Name + GetStatusDisplay(status,
                out ConsoleColor background,
                out ConsoleColor foreground);

            name = AlignNameWidth(name);

            string bar = GetBar(solder.GetShareHealth());

            int left = _solders[solder].Left;
            int top = _solders[solder].Top;

            ChangeColor(background, foreground);

            WriteLine(name, left, top);
            top++;
            WriteLine(bar, left, top);

            Console.ResetColor();
        }

        private void WriteLine(string text, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }

        private string GetStatusDisplay(
            SolderStatus status,
            out ConsoleColor background,
            out ConsoleColor foreground)
        {
            switch (status)
            {
                case SolderStatus.None:
                    foreground = ConsoleColor.DarkRed;
                    background = ConsoleColor.Gray;
                    return " Живой";

                case SolderStatus.Attacking:
                    foreground = ConsoleColor.Black;
                    background = ConsoleColor.Green;
                    return " Атакующий";

                case SolderStatus.Attacked:
                    foreground = ConsoleColor.Black;
                    background = ConsoleColor.Yellow;
                    return " Атакуемый";

                case SolderStatus.Dead:
                    foreground = ConsoleColor.DarkGray;
                    background = ConsoleColor.Gray;
                    return " Мертвый";

                default:
                    foreground = ConsoleColor.Black;
                    background = ConsoleColor.Black;
                    return string.Empty;
            }
        }

        public void ShowEndScreen(Platoon firstPlatoon, Platoon secondPlatoon)
        {
            string finalMessage;

            if (firstPlatoon.IsAlive == false && secondPlatoon.IsAlive == false)
            {
                finalMessage = "Оба отряда мертвы. Ничья.";
            }
            else if (firstPlatoon.IsAlive == false)
            {
                finalMessage = "Первый отряд пал. Второй победил!";
            }
            else
            {
                finalMessage = "Второй отряд пал. Первый победил!";
            }

            Console.WriteLine(finalMessage);
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
    }
}
