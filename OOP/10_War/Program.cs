using System;
using System.Collections.Generic;

namespace _10_War
{
    public class War
    {
        static void Main()
        {
            List<Solder> firstSolders = new List<Solder>
            {
                new BigGuy(),
                new Strongman(),
                new BigGuy(),
                new Strongman(),
                new ToughGuy(),
            };

            List<Solder> secondSolders = new List<Solder>
            {
                new BigGuy(),
                new Strongman(),
                new ToughGuy(),
                new Strongman(),
                new ToughGuy(),
            };

            Platoon firstPlatoon = new Platoon(firstSolders);
            Platoon secondPlatoon = new Platoon(secondSolders);

            Battlefield battlefield = new Battlefield(firstPlatoon, secondPlatoon);

            battlefield.StartBattle();
        }
    }

    public class BigGuy : Solder
    {
        public BigGuy() : base("Здоровяк", health: 120) { }
    }

    public class Strongman : Solder
    {
        public Strongman() : base("Силач", minDamage: 14, maxDamage: 19) { }
    }

    public class ToughGuy : Solder
    {
        public ToughGuy() : base("Крепыш", armor: 10) { }
    }

    public class Battlefield
    {
        private Platoon _firstPlatoon;
        private Platoon _secondPlatoon;

        public Battlefield(Platoon first, Platoon second)
        {
            _firstPlatoon = first;
            _secondPlatoon = second;
        }

        public void StartBattle()
        {
            while (_firstPlatoon.IsAlive() && _secondPlatoon.IsAlive())
            {
                ExchangeBlows(_firstPlatoon, _secondPlatoon);
                ExchangeBlows(_secondPlatoon, _firstPlatoon);
            }

            Console.Clear();

            if (_firstPlatoon.IsAlive() == false && _secondPlatoon.IsAlive() == false)
            {
                Console.WriteLine("Оба отряда мертвы. Ничья.");
            }
            else if (_firstPlatoon.IsAlive() == false)
            {
                Console.WriteLine("Первый отряд пал. Второй победил!");
            }
            else
            {
                Console.WriteLine("Второй отряд пал. Первый победил!");
            }

            Console.ReadKey();
        }

        private void ExchangeBlows(Platoon first, Platoon second)
        {
            if (first.IsAlive())
            {
                first.Attack(second.GetAttacked());
            }

            ShowGameScreen();
        }

        private void ShowGameScreen()
        {
            int maxNumberOfSolders = GetMaxPlatoonSize();
            int indentationLength = 7;
            string indentation = new string(' ', indentationLength);

            for (int i = 0; i < maxNumberOfSolders; i++)
            {
                _firstPlatoon.ShowNameByIndex(i);
                Console.Write(indentation);
                _secondPlatoon.ShowNameByIndex(i);
                Console.WriteLine();

                _firstPlatoon.ShowBarByIndex(i);
                Console.Write(indentation);
                _secondPlatoon.ShowBarByIndex(i);

                Console.WriteLine("\n");
            }

            Console.ReadKey();
            Console.Clear();
        }

        private int GetMaxPlatoonSize()
        {
            return _firstPlatoon.Count >= _secondPlatoon.Count ? _firstPlatoon.Count : _secondPlatoon.Count;
        }
    }

    public class Platoon
    {
        private List<Solder> _solders;

        public Platoon(List<Solder> solders)
        {
            _solders = solders;
        }

        private Solder Attacking { get; set; }
        private Solder Attacked { get; set; }
        private Solder LastSolder { get; set; }
        public int Count => _solders.Count;
        public int BarSize => 30;
        private char EmptySymbol => ' ';

        public void Attack(Solder enemy)
        {
            Attacked = null;
            Attacking = GetRandomSolder();
            Attacking.Attack(enemy);
        }

        public Solder GetAttacked()
        {
            Attacking = null;
            Attacked = GetRandomSolder();
            return Attacked;
        }

        public void ShowNameByIndex(int index)
        {
            if (index < _solders.Count)
            {
                string name = _solders[index].Name;

                if (_solders[index].IsALive == false)
                {
                    name = AlignNameWidth(name);
                    ShowColoredText(name, ConsoleColor.White, ConsoleColor.Black);
                }
                else if (_solders[index] == Attacking)
                {
                    string attacking = " (атакующий)";
                    name = AlignNameWidth(name, attacking);
                    ShowColoredText(name, ConsoleColor.DarkRed, ConsoleColor.Black);
                }
                else if (_solders[index] == Attacked)
                {
                    string attacked = " (атакуемый)";
                    name = AlignNameWidth(name, attacked);
                    ShowColoredText(name, ConsoleColor.DarkBlue, ConsoleColor.White);
                }
                else
                {
                    name = AlignNameWidth(name);
                    Console.Write(name);
                }
            }
            else
            {
                Console.Write(new string(EmptySymbol, BarSize));
            }
        }

        public void ShowBarByIndex(int index)
        {
            if (index < _solders.Count)
            {
                float health = _solders[index].Health;
                float lastHealth = _solders[index].LastHealth;
                string firstHalfBar = GetHalvesBar(health, lastHealth, out string secondHalfBar);

                if (_solders[index].IsALive == false)
                {
                    string deadMessage = "Этот юнит мертв!";
                    ShowColoredText(deadMessage, ConsoleColor.White, ConsoleColor.Black);
                    Console.Write(new string(EmptySymbol, BarSize - deadMessage.Length));
                }
                else if (_solders[index] == Attacking)
                {
                    ShowColoredText(firstHalfBar, ConsoleColor.DarkRed, ConsoleColor.Black);
                    ShowColoredText(secondHalfBar, ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
                }
                else if (_solders[index] == Attacked)
                {
                    ShowColoredText(firstHalfBar, ConsoleColor.DarkBlue, ConsoleColor.Black);
                    ShowColoredText(secondHalfBar, ConsoleColor.DarkYellow, ConsoleColor.DarkYellow);
                }
                else
                {
                    ShowColoredText(firstHalfBar, ConsoleColor.Green, ConsoleColor.DarkGreen);
                    ShowColoredText(secondHalfBar, ConsoleColor.DarkGray, ConsoleColor.Gray);
                }
            }
            else
            {
                Console.Write(new string(EmptySymbol, BarSize));
            }
        }

        public bool IsAlive()
        {
            int numberLivingSolders = 0;

            foreach (Solder solder in _solders)
            {
                if (solder.IsALive)
                {
                    numberLivingSolders++;
                }
            }

            return numberLivingSolders > 0;
        }

        private Solder GetRandomSolder()
        {
            List<Solder> liaveSolders = new List<Solder>();
            int numberLastSolder = 1;

            for (int i = 0; i < _solders.Count; i++)
            {
                if (_solders[i].IsALive)
                {
                    liaveSolders.Add(_solders[i]);
                }
            }

            if (liaveSolders.Count <= numberLastSolder)
            {
                LastSolder = liaveSolders[0];
                return LastSolder;
            }
            else
            {
                return liaveSolders[UserUtils.GenerateRandomNumber(liaveSolders.Count)];
            }
        }

        private string GetHalvesBar(float health, float lastHealth, out string secondHalfBar)
        {
            if (lastHealth < 0)
            {
                secondHalfBar = "\0";
                return new string(EmptySymbol, BarSize);
            }
            else
            {
                float percent = lastHealth / health * BarSize;
                secondHalfBar = new string(EmptySymbol, BarSize - (int)percent);
                return new string(EmptySymbol, (int)percent);
            }
        }

        private void ShowColoredText(string message, ConsoleColor targetColor, ConsoleColor textColor)
        {
            Console.BackgroundColor = targetColor;
            Console.ForegroundColor = textColor;
            Console.Write(message);
            Console.ResetColor();
        }

        private string AlignNameWidth(string name, string additionalText = "\0")
        {
            name += additionalText;
            name = name.Length < BarSize ? name.PadRight(BarSize) : name.Remove(BarSize);
            return name;
        }
    }

    public class Solder
    {
        private readonly string _name;
        private readonly float _health;
        private float _lastHealth;
        private int _minDamage;
        private int _maxDamage;
        private float _armor;

        public Solder(
            string name,
            float health = 100,
            int minDamage = 30,
            int maxDamage = 45,
            float armor = 7
            )
        {
            _name = name;
            _health = health;
            _lastHealth = _health;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _armor = armor;
        }


        public string Name => _name;
        public bool IsALive => _lastHealth > 0;
        public float Health => _health;
        public float LastHealth => _lastHealth;
        public float Damage => UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);

        public void Attack(Solder enemy)
        {
            enemy.TakeDamage(Damage);
        }

        public void TakeDamage(float damage)
        {
            int coefficient = 2;

            if (damage > 0 && damage <= _armor)
            {
                _lastHealth -= damage / coefficient;
            }
            else if (damage > 0 && damage > _armor)
            {
                _lastHealth -= _armor / coefficient + damage - _armor;
            }
        }
    }

    public abstract class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static int GenerateRandomNumber(int max)
        {
            return s_random.Next(max);
        }
    }
}
