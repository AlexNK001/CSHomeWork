
namespace _7_Passenger_train_configurator
{
    public class Program
    {
        private static void Main()
        {
            CitiesStorage cities = new CitiesStorage("Москва", "Пермь", "Омск", "Казань", "Уфа", "Владивосток", "Рязань");
            TrainFactory creator = new TrainFactory(cities);
            Station station = new Station(creator);
            station.Work();
        }
    }
}
