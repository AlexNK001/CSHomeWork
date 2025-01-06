
namespace _7_Passenger_train_configurator
{
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
}
