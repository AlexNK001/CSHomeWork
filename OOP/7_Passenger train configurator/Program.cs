using System;
using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public class Program
    {
        static void Main()
        {
            Citys citys = new Citys("Москва", "Пермь", "Омск", "Казань", "Уфа", "Владивосток", "Рязань");

            //Station station = new Station();
            //station.Work();
            Trains trains = new Trains();
            Train train = trains.Create();
            TextStorage.ShowFullInfo(train);
            TextStorage.TestShow(train);


            //Directions directions = new Directions();
            //Direction direction = directions.Create();
            //Console.WriteLine($"Направление создано {direction.StartPoint} {direction.EndPoint}");
        }
    }

    public class Station
    {
        private readonly List<Train> _trains;

        public Station()
        {
            _trains = new List<Train>();
        }

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                TextStation.ShowMainMenu(_trains.Count);

                switch (Console.ReadLine())
                {
                    case TextStation.CommandAddTrain:
                        //AddTrain();
                        break;

                    case TextStation.CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }

                Console.Clear();
            }
        }
    }

    public struct Train
    {
        public readonly int Number;
        public readonly Direction Direction;
        public readonly IReadOnlyList<TrainCar> TrainCars;

        public Train(int number, Direction direction, IReadOnlyList<TrainCar> trainCars)
        {
            Number = number;
            Direction = direction;
            TrainCars = trainCars;
        }

        public int GetCountPassanger()
        {
            int result = 0;

            for (int i = 0; i < TrainCars.Count; i++)
                result += TrainCars[i].CountPassengers;

            return result;
        }
    }

    public struct TrainCar
    {
        public readonly int Capacity;
        public readonly int CountPassengers;

        public TrainCar(int capacity, int countPassengers)
        {
            Capacity = capacity;
            CountPassengers = countPassengers;
        }
    }

    public struct Direction
    {
        public readonly string StartPoint;
        public readonly string EndPoint;

        public Direction(string startPoint, string endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }

    public class Trains
    {
        private Random s_random;
        private int _currentName;
        private readonly int[] _capacityCars;

        public Trains(Random random)
        {
            s_random = random;
            _currentName = 0;
            _capacityCars = new int[] { 10, 20, 30, 100 };
        }

        public Train Create()
        {
            int number = ++_currentName;
            int countPassanger = s_random.Next(100, 500);

            List<TrainCar> trainCars = new List<TrainCar>();
            TrainCars trainCreator = new TrainCars();
            Directions directions = new Directions(new Citys());//

            for (int i = 0; i < countPassanger; i++)
            {
                int capacityCars = _capacityCars[s_random.Next(0, _capacityCars.Length)];
                TrainCar trainCar = trainCreator.Create(capacityCars, countPassanger);
                countPassanger -= trainCar.CountPassengers;
                trainCars.Add(trainCar);
            }

            Direction direction = directions.Create();

            return new Train(number, direction, trainCars);
        }
    }

    public class TrainCars
    {
        public TrainCar Create(int capacity, int countPassanger)
        {
            if (countPassanger > capacity)
                countPassanger = capacity;

            return new TrainCar(capacity, countPassanger);
        }
    }

    public class Directions
    {
        //private readonly string[] _citys;

        private Citys _citys1;
        public Directions(Citys citys)
        {
            _citys1 = citys;
            //_citys = GetCitys();
        }

        //private void ShowCitys()
        //{
        //    int enumerator = 1;

        //    foreach (var city in _citys)
        //    {
        //        Console.WriteLine($"{enumerator}) {city}");
        //        enumerator++;
        //    }
        //}

        public Direction Create()
        {
            string startPoint = string.Empty;
            string endPoint = string.Empty;
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                //ShowCitys();
                Console.WriteLine("Выберите первый город.");

                if (TrySelect(out startPoint) == false)
                {
                    Console.WriteLine("Неверно выбран первый город.");
                    continue;
                }

                Console.WriteLine("Выберите второй город.");

                if (TrySelect(out endPoint) == false)
                {
                    Console.WriteLine("Неверно выбран второй город.");
                    continue;
                }

                if (startPoint != endPoint)
                {
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Маршрут должен быть из разных городов!");
                }

                Console.ReadKey();
            }

            return new Direction(startPoint, endPoint);
        }

        private bool TrySelect(out string point)
        {
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                value--;
                bool isIndex = value >= 0 && value < _citys1.Length;
                point = isIndex ? _citys1[value] : string.Empty;
                return isIndex;
            }
            else
            {
                point = string.Empty;
                return false;
            }
        }

        //private string[] GetCitys()
        //{
        //    string[] citys = {
        //    "Москва",
        //    "Пермь",
        //    "Омск",
        //    "Казань",
        //    "Уфа",
        //    "Владивосток",
        //    "Рязань",
        //    };

        //    return citys;
        //}
    }

    public static class TextStorage
    {
        public static void ShowFullInfo(Train train)
        {
            Console.WriteLine($"" +
                $"train.Number:{train.Number} " +
                $"train.GetCountPassanger():{train.GetCountPassanger()} " +
                $"train.Direction.StartPoint:{train.Direction.StartPoint} " +
                $"train.Direction.EndPoint:{train.Direction.EndPoint} " +
                $"train.TrainCars.Count:{train.TrainCars.Count}");
        }

        public static void TestShow(Train train)
        {
            for (int i = 0; i < train.TrainCars.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {train.TrainCars[i].Capacity}/{train.TrainCars[i].CountPassengers}");
            }
        }
    }
}
