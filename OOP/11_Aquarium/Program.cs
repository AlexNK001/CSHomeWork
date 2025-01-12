using System;
using System.Collections.Generic;

namespace _11_Aquarium
{
    public class Program
    {
        private static void Main()
        {
            int time = 12;
            int fishCount = 5;
            FishCreator creator = new FishCreator();
            List<Fish> fishes = creator.GiveStartingFishes(fishCount, time);
            Aquarium aquarium = new Aquarium(fishes, creator.GetMaxNameLength(), time);
            Terrarium terrarium = new Terrarium(time, creator, aquarium);
            terrarium.InteractAquarium();
        }
    }

    public class Terrarium
    {
        private int _time;
        private readonly FishCreator _fishCreator;
        private readonly Aquarium _aquarium;

        public Terrarium(int time, FishCreator fishCreator, Aquarium aquarium)
        {
            _time = time;
            _fishCreator = fishCreator;
            _aquarium = aquarium;
        }

        public void InteractAquarium()
        {
            const string CommandShowFishes = "1";
            const string CommandAddFish = "2";
            const string CommandDeleteFish = "3";
            const string CommandRemoveDeadFishes = "4";
            const string CommandAddTime = "5";
            const string CommandExit = "6";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Текущее время: {_time}\n");
                Console.WriteLine($"{CommandShowFishes} - Показать всех рыб.");
                Console.WriteLine($"{CommandAddFish} - Добавить рыбу.");
                Console.WriteLine($"{CommandDeleteFish} - Удалить рыбу.");
                Console.WriteLine($"{CommandRemoveDeadFishes} - Убрать всех мёртвых рыб.");
                Console.WriteLine($"{CommandAddTime} - Подождать (прибавить время).");
                Console.WriteLine($"{CommandExit} - Выход.");

                switch (Console.ReadLine())
                {
                    case CommandShowFishes:
                        _aquarium.ShowFishes(_time);
                        break;

                    case CommandAddFish:
                        AddFish();
                        break;

                    case CommandDeleteFish:
                        _aquarium.DeleteFish(_time);
                        break;

                    case CommandRemoveDeadFishes:
                        _aquarium.RemoveDeadFishes(_time);
                        break;

                    case CommandAddTime:
                        ++_time;
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Введена неверная команда!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddFish()
        {
            if (_aquarium.HaveFreePlaces)
            {
                _aquarium.AddFish(_fishCreator.CreateFish(_time));
            }
        }
    }

    public class Fish
    {
        private readonly int _timeBirth;

        public Fish(string name, int maxAge, int time = 0)
        {
            Name = name;
            MaxAge = maxAge;
            _timeBirth = time;
        }

        public string Name { get; private set; }
        public int MaxAge { get; private set; }

        public Fish Clone(int time)
        {
            return new Fish(Name, MaxAge, time);
        }

        public int GetAge(int time)
        {
            return time - _timeBirth;
        }

        public bool IsAlive(int time)
        {
            return time < _timeBirth + MaxAge;
        }
    }

    public class Aquarium
    {
        private readonly List<Fish> _fishes;
        private readonly int _maxCount;
        private readonly int _nameLength;

        public Aquarium(List<Fish> fishes, int nameLength, int maxCount = 20)
        {
            _fishes = fishes;
            _nameLength = nameLength;
            _maxCount = maxCount;
        }

        public bool HaveFreePlaces => _fishes.Count < _maxCount;

        public void ShowFishes(int time)
        {
            int number = 1;
            string separator = " ";
            int numberCharacters = 2;

            foreach (Fish fish in _fishes)
            {
                string text = number.ToString().PadLeft(numberCharacters) + separator;
                text += fish.Name.PadRight(_nameLength) + separator;
                text += fish.GetAge(time).ToString().PadLeft(numberCharacters) + "/";
                text += fish.MaxAge.ToString().PadLeft(numberCharacters) + separator;
                text = fish.IsAlive(time) ? text += "живая" : text += "мёртвая";

                Console.WriteLine(text);
                number++;
            }
        }

        public void AddFish(Fish fish)
        {
            _fishes.Add(fish);
        }

        public void DeleteFish(int time)
        {
            if (_fishes.Count > 0)
            {
                int minValue = 1;
                int maxValue = _fishes.Count;

                Console.WriteLine("Выберите рыбку из списка.\n");
                ShowFishes(time);

                int index = UserUtils.GetNumberFromRange(minValue, maxValue);
                index--;

                _fishes.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Нечего удалять!");
            }
        }

        public void RemoveDeadFishes(int time)
        {
            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                if (_fishes[i].IsAlive(time) == false)
                {
                    _fishes.Remove(_fishes[i]);
                }
            }
        }
    }

    public class FishCreator
    {
        private readonly List<Fish> _fishes;

        public FishCreator()
        {
            _fishes = GetFishes();
        }

        public Fish CreateFish(int time)
        {
            int minValue = 1;
            int maxValue = _fishes.Count;

            Console.WriteLine("Выберите рыбку из списка.\n");
            ShowAvailable();

            int index = UserUtils.GetNumberFromRange(minValue, maxValue);
            index--;

            return _fishes[index].Clone(time);
        }

        public int GetMaxNameLength()
        {
            int length = 0;

            foreach (var item in _fishes)
            {
                if (item.Name.Length > length)
                {
                    length = item.Name.Length;
                }
            }

            return length;
        }

        public List<Fish> GiveStartingFishes(int count, int time)
        {
            List<Fish> fishes = new List<Fish>();

            for (int i = 0; i < count; i++)
            {
                fishes.Add(_fishes[UserUtils.GenerateRandomNumber(_fishes.Count)].Clone(time));
            }

            return fishes;
        }

        private void ShowAvailable()
        {
            int number = 1;

            foreach (Fish fish in _fishes)
            {
                Console.WriteLine($"{number} {fish.Name}");
                number++;
            }
        }

        private List<Fish> GetFishes()
        {
            List<Fish> fishes = new List<Fish>
            {
                new Fish("Петушок", 10),
                new Fish("Скалярия", 17),
                new Fish("Гуппи", 14),
                new Fish("Гурами", 15),
                new Fish("Дискус", 19),
                new Fish("Золотая рыбка", 11),
                new Fish("Лабео", 12),
                new Fish("Комета", 16)
            };

            return fishes;
        }
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GetNumberFromRange(int min, int max)
        {
            bool isLookingResult = true;
            int result = 0;

            while (isLookingResult)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= min && result <= max)
                    {
                        isLookingResult = false;
                    }
                    else
                    {
                        Console.WriteLine("Введенное число не входит в диапазон!");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод!");
                }
            }

            return result;
        }

        public static int GenerateRandomNumber(int max)
        {
            return s_random.Next(max);
        }
    }
}
