using System;
using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public class Program
    {
        static void Main()
        {
            Station station = new Station();
            station.Work();
        }
    }

    public class Station
    {
        static private Random s_random;

        private List<Train> _trains;
        private List<int> _trainsName;

        public Station()
        {
            s_random = new Random();
            _trains = new List<Train>();
            _trainsName = new List<int>();
        }

        public void Work()
        {
            const string CommandAddTrain = "1";
            const string CommandShowSelectedTrain = "2";
            const string CommandExit = "3";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Количество созданных поездов {_trains.Count}.\n");

                Console.WriteLine($"{CommandAddTrain} - Создать маршрут.");
                Console.WriteLine($"{CommandShowSelectedTrain} - Показать подробную информацию по выбранному маршруту.");
                Console.WriteLine($"{CommandExit} - Выход из программы.\n");

                if (_trains.Count > 0)
                {
                    foreach (Train train in _trains)
                    {
                        train.ShowBriefInfo();
                    }
                }
                else
                {
                    Console.WriteLine("Поездов на станции пока нет.");
                }

                switch (Console.ReadLine())
                {
                    case CommandAddTrain:
                        AddTrain();
                        break;

                    case CommandShowSelectedTrain:
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }

                Console.Clear();
            }
        }

        private void AddTrain()
        {
            int minRandom = 50;
            int maxRandom = 550;

            int trainName = GetTrainName();

            int countPassanger = s_random.Next(minRandom, maxRandom);

            Directions directions = new Directions();

            _trains.Add(new Train(trainName, countPassanger, directions));
        }

        private int GetTrainName()
        {
            int minRandom = 100;
            int maxRandom = 1000;
            bool isWork = true;
            int number = 0;

            if (_trainsName.Count >= maxRandom - minRandom)
            {
                int tempNumber = minRandom;
                minRandom = maxRandom;
                maxRandom += tempNumber;
            }

            while (isWork)
            {
                number = s_random.Next(minRandom, maxRandom);

                if (_trainsName.Contains(number) == false)
                {
                    _trainsName.Add(number);
                    isWork = false;
                }
            }

            return number;
        }
    }

    public class Train
    {
        static private Random s_random;

        private int _name;
        private int _countPassanger;
        private Directions _directions;
        private List<TrainCar> _cars;

        public Train(int name, int countPassanger, Directions directions)
        {
            s_random = new Random();
            _name = name;
            _countPassanger = countPassanger;
            _cars = AddCars(countPassanger);
            _directions = directions;
        }

        private int[] CapacityCars => new int[] { 10, 20, 30, 100 };

        public void ShowFullInfo()
        {
            Console.Write($"Поезд под номером {_name} ");
            Console.Write($"Маршрут {_directions.Name} ");
            Console.WriteLine($"Количество вагонов {_cars.Count}");

            foreach (TrainCar cars in _cars)
            {
                cars.ShowInfo();
            }
        }

        public void ShowBriefInfo()
        {
            int firstIndentation = 35;
            int secondIndentation = 17;
            int thirdIndenttation = 17;

            string trainName = $"Поезд под номером: {_name} |";
            string direction = GetIndentation($"Маршрут: {_directions.Name} |", firstIndentation);
            string carsCount = GetIndentation($"Вагонов: {_cars.Count} |", secondIndentation);
            string countPassanger = GetIndentation($"Пасажиров: {_countPassanger} |", thirdIndenttation);

            Console.WriteLine($"{trainName}{direction}{carsCount}{countPassanger}");

            string GetIndentation(string text, int indentation)
            {
                if (text.Length > indentation)
                {
                    indentation = text.Length;
                }

                return new string(' ', indentation - text.Length) + text;
            }
        }

        private List<TrainCar> AddCars(int countPassengers)
        {
            List<TrainCar> cars = new List<TrainCar>();
            int carNumber = 1;

            while (countPassengers > 0)
            {
                int capacity = GetRandomCapacity();
                TrainCar trainCar = new TrainCar(carNumber, countPassengers, capacity);
                carNumber++;

                countPassengers -= trainCar.CountPassengers;

                cars.Add(trainCar);
            }

            return cars;
        }

        private int GetRandomCapacity()
        {
            int index = s_random.Next(CapacityCars.Length);
            return CapacityCars[index];
        }
    }

    public class TrainCar
    {
        private int _name;
        private int _capacity;
        private int _countPassengers;

        public TrainCar(int name, int countPassengers, int capacity)
        {
            _name = name;
            _capacity = capacity;
            _countPassengers = BoardPassengers(countPassengers);
        }

        public int CountPassengers => _countPassengers;

        public void ShowInfo()
        {
            Console.WriteLine($"Номер вагона - {_name}. Вместимость - {_capacity}/{_countPassengers}");
        }

        private int BoardPassengers(int countPassanger)
        {
            return countPassanger >= _capacity ? _capacity : countPassanger;
        }
    }

    public class Directions
    {
        private string[] _citys;

        public Directions()
        {
            _citys = GetCitys();
            Name = GetName();
        }

        public string Name { get; private set; }

        private void ShowCitys()
        {
            int enumerator = 1;

            foreach (var city in _citys)
            {
                Console.WriteLine($"{enumerator}) {city}");
                enumerator++;
            }
        }

        private string GetName()
        {
            bool isWork = true;
            string result = null;

            while (isWork)
            {
                ShowCitys();
                Console.WriteLine("Выберите первый город.");

                if (TrySelect(out string startPoint, out int firstIndex))
                {
                    Console.WriteLine("Выберите второй город.");

                    if (TrySelect(out string endPoint, out int secondIndex))
                    {
                        if (firstIndex != secondIndex)
                        {
                            result = $"{startPoint} / {endPoint}";
                            isWork = false;
                        }
                        else
                        {
                            Console.WriteLine("Маршрут должен быть из разных городов!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверно выбран второй город.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверно выбран первый город.");
                }

                Console.ReadKey();
                Console.Clear();
            }

            return result;
        }

        private bool TrySelect(out string point, out int index)
        {
            point = null;
            index = 0;

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                index = value;
                value--;

                if (value >= 0 && value < _citys.Length)
                {
                    point = _citys[value];
                    index = value;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private string[] GetCitys()
        {
            string[] citys = {
            "Москва",
            "Пермь",
            "Омск",
            "Казань",
            "Уфа",
            "Владивосток",
            "Рязань",
            };

            return citys;
        }
    }
}
