
using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public class TrainCarCreator
    {
        private readonly int[] _capacityCars;

        public TrainCarCreator()
        {
            _capacityCars = new int[] { 10, 20, 30, 100 };
        }

        public List<TrainCar> Create(int countPassanger)
        {
            List<TrainCar> cars = new List<TrainCar>();

            while (countPassanger > 0)
            {
                int capacityCars = _capacityCars[UserUtils.GenerateRandomNumber(_capacityCars.Length)];
                int currentCountPassanger = countPassanger > capacityCars ? capacityCars : countPassanger;

                TrainCar trainCar = new TrainCar(capacityCars, currentCountPassanger);
                cars.Add(trainCar);

                countPassanger -= trainCar.CountPassengers;
            }

            return cars;
        }
    }
}
