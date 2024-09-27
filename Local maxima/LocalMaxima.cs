using System;

namespace Local_maxima
{
    internal class LocalMaxima
    {
        private static void Main()
        {
            int minRandomNumber = 0;
            int maxRandomNumber = 101;
            Random random = new Random();

            int[] numbers = new int[30];
            int numberOfChecks = numbers.Length - 1;

            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = random.Next(minRandomNumber, maxRandomNumber);

            if (numbers[numbers.GetLowerBound(0)] > numbers[numbers.GetLowerBound(0) + 1])
                Console.WriteLine($"{numbers[numbers.GetLowerBound(0)]} - Локальный максимум");
            else
                Console.WriteLine(numbers[numbers.GetLowerBound(0)]);

            for (int i = 1; i < numberOfChecks; ++i)
            {
                if (numbers[i] > numbers[i - 1] && numbers[i] > numbers[i + 1])
                    Console.WriteLine($"{numbers[i]} - Локальный максимум");
                else
                    Console.WriteLine(numbers[i]);
            }

            if (numbers[numbers.GetUpperBound(0)] > numbers[numbers.GetUpperBound(0) - 1])
                Console.WriteLine($"{numbers[numbers.GetUpperBound(0)]} - Локальный максимум");
            else
                Console.WriteLine(numbers[numbers.GetUpperBound(0)]);

            Console.ReadKey();
        }
    }
}
