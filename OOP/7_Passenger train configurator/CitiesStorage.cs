
namespace _7_Passenger_train_configurator
{
    public class CitiesStorage
    {
        private readonly string[] _citys;

        public CitiesStorage(params string[] citys)
        {
            _citys = citys;
        }

        public int Length => _citys.Length;
        public string this[int index] => _citys[index];
    }
}
