using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public struct Train
    {
        public readonly int Number;
        public readonly Direction Direction;
        private readonly List<TrainCar> _trainCars;

        public Train(int number, Direction direction, List<TrainCar> trainCars)
        {
            Number = number;
            Direction = direction;
            _trainCars = trainCars;
        }

        public int Count => _trainCars.Count;
        public TrainCar this[int index] => _trainCars[index];

        public int GetCountPassanger()
        {
            int result = 0;

            for (int i = 0; i < _trainCars.Count; i++)
                result += _trainCars[i].CountPassengers;

            return result;
        }
    }
}
