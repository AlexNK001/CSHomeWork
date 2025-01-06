using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public class TrainsCreator
    {
        private int _currentNumber;

        public TrainsCreator()
        {
            _currentNumber = 0;
        }

        public Train Create(Direction direction, List<TrainCar> trainCars)
        {
            int number = ++_currentNumber;

            return new Train(number, direction, trainCars);
        }
    }
}
