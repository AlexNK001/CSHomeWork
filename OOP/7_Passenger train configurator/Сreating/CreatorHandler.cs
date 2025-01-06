using System.Collections.Generic;

namespace _7_Passenger_train_configurator
{
    public class CreatorHandler
    {
        private readonly int _minCoutPassanger;
        private readonly int _maxCoutPassanger;

        private readonly DirectionCreator  _directionCreator;
        private readonly TrainCarCreator _trainCarCreator;
        private readonly TrainsCreator _trainCreator;

        public CreatorHandler(CitiesStorage citys, int minCoutPassanger = 100, int maxCoutPassanger = 500)
        {
            _directionCreator = new DirectionCreator (citys);
            _trainCarCreator = new TrainCarCreator();
            _trainCreator = new TrainsCreator();

            _minCoutPassanger = minCoutPassanger;
            _maxCoutPassanger = maxCoutPassanger;
        }

        public Train Create()
        {
            Direction direction = _directionCreator.Create();
            int countPassanger = UserUtils.GenerateRandomNumber(_minCoutPassanger, _maxCoutPassanger);
            List<TrainCar> cars = _trainCarCreator.Create(countPassanger);

            return _trainCreator.Create(direction, cars);
        }
    }
}
