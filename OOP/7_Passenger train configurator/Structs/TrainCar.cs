
namespace _7_Passenger_train_configurator
{
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
}
