using System;

namespace power_of_two
{
    internal class Program
    {
        private static void Main()
        {
            int minRandomValue = 1;
            int maxRandomValue = 28;
            int counter = 0;
            int minValue = 100;
            int maxValue = 1000;
            Random random = new Random();
            int number = random.Next(minRandomValue, maxRandomValue);

            for (int i = number; i < maxValue; i += number)
            {
                if (i >= minValue)
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}
