using System;

namespace Multiples
{
    class Program
    {
        static void Main()
        {
            int minRandomValue = 10;
            int maxRandomValue = 25;
            int counter = 0;
            int minValue = 50;
            int maxValue = 150;
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