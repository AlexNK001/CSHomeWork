using System;

namespace Polyclinic
{
    class Program
    {
        static void Main()
        {
            int numberMinutesInHour = 60;
            int receptionTime = 10;

            Console.Write("Введите количество людей в очереди");
            int numberPeople = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            int allTimeInMinutes = numberPeople * receptionTime;
            int finalTimeInMinutes = allTimeInMinutes % numberMinutesInHour;
            int finalTimeInHours = allTimeInMinutes / numberMinutesInHour;

            Console.WriteLine($"Время ожидания {finalTimeInHours} часа и {finalTimeInMinutes} минут.");
            Console.ReadKey();
        }
    }
}
