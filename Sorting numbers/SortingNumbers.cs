using System;

namespace Sorting_numbers
{
    internal class SortingNumbers
    {
        private static void Main()
        {
            Random random = new Random();
            int minRandom = 0;
            int maxRandom = 11;
            int[] numbers = new int[15];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandom, maxRandom);
                Console.Write($"{numbers[i]} ");
            }

            Console.WriteLine();

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1 - i; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int tempNumber = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = tempNumber;
                    }
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"{numbers[i]} ");
            }
        }
    }
}
