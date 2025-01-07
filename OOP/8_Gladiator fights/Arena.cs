using System;
using System.Collections.Generic;

namespace _8_Gladiator_fights
{
    public class Arena
    {
        private readonly List<Warrior> _warriors;
        private Warrior _firstWarrior;
        private Warrior _secondWarrior;
        private bool _isReady = false;
        private readonly ConsoleColor _firstColor = ConsoleColor.Red;
        private readonly ConsoleColor _secondColor = ConsoleColor.Blue;

        public Arena()
        {
            _warriors = GetWarriors();
        }

        public void Work()
        {
            const string CommmandSelectPlayers = "1";
            const string CommandStartBattle = "2";
            const string CommmandExit = "3";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{CommmandSelectPlayers} - выбрать игроков");
                Console.WriteLine($"{CommandStartBattle} - начать битву");
                Console.WriteLine($"{CommmandExit} - выйти из игры");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommmandSelectPlayers:
                        _isReady = TrySelectWarriors();
                        break;

                    case CommandStartBattle:
                        StartBattle();
                        break;

                    case CommmandExit:
                        isWork = false;
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void StartBattle()
        {
            bool isWork = true;

            if (_isReady)
            {
                while (isWork)
                {
                    ExchangeBlows();
                    isWork = HaveLiveWarriors();
                    Console.ReadKey();
                }

                _isReady = false;
            }
            else
            {
                Console.WriteLine("Выберите игроков");
            }
        }

        private bool TrySelectWarriors()
        {
            ShowNames();

            Console.WriteLine("Выберите первого игрока.");

            if (TrySelectWarrior(out Warrior firstWarrior))
            {
                Console.WriteLine("Выберите второго игрока.");

                if (TrySelectWarrior(out Warrior secondWarrior))
                {
                    _firstWarrior = firstWarrior.Clone();
                    _secondWarrior = secondWarrior.Clone();

                    _isReady = true;

                    return true;
                }
            }

            return false;
        }

        private bool HaveLiveWarriors()
        {
            if (_firstWarrior.IsALive == false && _secondWarrior.IsALive == false)
            {
                Console.WriteLine("Игроки убили друг друга. Ничья.");
            }
            else if (_firstWarrior.IsALive == false)
            {
                Console.WriteLine($"Первый игрок: {_firstWarrior.Name} пал. Второй игрок: {_secondWarrior.Name} выйграл!");
            }
            else if (_secondWarrior.IsALive == false)
            {
                Console.WriteLine($"Второй игрок: {_secondWarrior.Name} пал. Первый игрок: {_firstWarrior.Name} выйграл!");
            }

            return _firstWarrior.IsALive && _secondWarrior.IsALive;
        }

        private void ExchangeBlows()
        {
            _firstWarrior.Attack(_secondWarrior);
            _secondWarrior.ShowInfo(_secondColor);

            Console.WriteLine();

            _secondWarrior.Attack(_firstWarrior);
            _firstWarrior.ShowInfo(_firstColor);
        }

        private bool TrySelectWarrior(out Warrior warrior)
        {
            warrior = null;

            bool haveInput = int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _warriors.Count;

            if (haveInput)
            {
                warrior = _warriors[index - 1];
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }

            return haveInput;
        }

        private List<Warrior> GetWarriors()
        {
            return new List<Warrior>
            {
                new Paladin(),
                new Archer(),
                new Wizard(),
                new Barbarian(),
                new SwordMaster(),
            };
        }

        private void ShowNames()
        {
            int number = 1;

            foreach (Warrior warrior in _warriors)
            {
                Console.WriteLine($"{number})- {warrior.Name}");
                number++;
            }
        }
    }
}
