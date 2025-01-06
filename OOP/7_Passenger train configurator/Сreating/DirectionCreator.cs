using System;

namespace _7_Passenger_train_configurator
{
    public class DirectionCreator 
    {
        private readonly CitiesStorage _citiesStorage;

        public DirectionCreator (CitiesStorage citys)
        {
            _citiesStorage = citys;
        }

        public Direction Create()
        {
            string startPoint = string.Empty;
            string endPoint = string.Empty;
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                TextStorage.ShowCities(_citiesStorage);

                if (ShowTrySelect(TextStorage.NumberCity.First, out startPoint))
                {
                    if (ShowTrySelect(TextStorage.NumberCity.Second, out endPoint) && startPoint != endPoint)
                    {
                        isWork = false;
                    }
                }

                Console.ReadKey();
            }

            return new Direction(startPoint, endPoint);
        }

        private bool ShowTrySelect(TextStorage.NumberCity text, out string point)
        {
            TextStorage.ShowCitySelection(text);
            bool isTry = TrySelect(out point);

            if (isTry == false)
            {
                TextStorage.ShowWrongCitySelection(text);
            }

            return isTry;
        }

        private bool TrySelect(out string point)
        {
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                value--;
                bool isIndex = value >= 0 && value < _citiesStorage.Length;
                point = isIndex ? _citiesStorage[value] : string.Empty;
                return isIndex;
            }
            else
            {
                point = string.Empty;
                return false;
            }
        }
    }
}
