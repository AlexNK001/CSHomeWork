using System;

namespace _7_Passenger_train_configurator
{
    public static class TextStorage
    {
        public const string CommandAddTrain = "1";
        public const string CommandExit = "2";

        private static readonly string _period = ".";
        private static readonly string _comma = ", ";
        private static readonly string _nextLine = ".\n";
        private static readonly string _slash = "/";
        private static readonly string _bracket = ")";

        public enum NumberCity
        {
            First,
            Second
        }

        public static void ShowCitySelection(NumberCity numberCity)
        {
            Console.WriteLine($"Выберите {GetNumberCity(numberCity)} город{_period}");
        }

        public static void ShowWrongCitySelection(NumberCity numberCity)
        {
            Console.WriteLine($"Неверно выбран {GetNumberCity(numberCity)} город{_period}");
        }

        private static string GetNumberCity(NumberCity numberCity)
        {
            switch (numberCity)
            {
                case NumberCity.First:
                    return "первый";

                case NumberCity.Second:
                    return "второй";

                default:
                    return string.Empty;
            }
        }

        public static void ShowCountTrains(int trainsCount)
        {
            Console.WriteLine($"Количество созданных поездов {trainsCount}{_nextLine}");
        }

        public static void ShowMainMenu()
        {
            string finalMessage = $"{CommandAddTrain} - Создать маршрут{_nextLine}";
            finalMessage += $"{CommandExit} - Выход из программы{_period}";

            Console.WriteLine(finalMessage);
        }

        public static void ReportIncorrectInput()
        {
            Console.WriteLine($"Неверный ввод{_period}");
        }

        public static void ShowFullInformation(Train train)
        {
            string finalMessage = GetBriefInformation(train, _nextLine);
            finalMessage += $"Колличество вагонов:{train.Count}{_nextLine}";

            for (int i = 0; i < train.Count; i++)
            {
                finalMessage += $"{i + 1}{_bracket}{train[i].Capacity}{_slash}{train[i].CountPassengers}{_nextLine}";
            }

            Console.WriteLine(finalMessage);
        }

        public static void ShowBriefInformation(Train train)
        {
            string finalMessage = GetBriefInformation(train, _comma);
            Console.WriteLine(finalMessage);
        }

        private static string GetBriefInformation(Train train, string separator)
        {
            string briefInformation = $"Номер поезда:{train.Number}{separator}";
            briefInformation += $"Направление:{train.Direction.StartPoint}{_slash}";
            briefInformation += $"{train.Direction.EndPoint}{separator}";
            briefInformation += $"Колличество пассажиров:{train.GetCountPassanger()}{separator}";

            return briefInformation;
        }

        public static void ShowCities(CitiesStorage cities)
        {
            string finalMessage = string.Empty;

            for (int i = 0; i < cities.Length; i++)
            {
                finalMessage += $"{i + 1}{_bracket}{cities[i]}{_nextLine}";
            }

            Console.WriteLine(finalMessage);
        }
    }
}
