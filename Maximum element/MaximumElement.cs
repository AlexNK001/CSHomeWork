using System;

namespace Maximum_element
{
    internal class MaximumElement
    {
        private static void Main()
        {
            int minRandomNumber = 0;
            int maxRandomNumber = 100;
            int maxElement = int.MinValue;
            int replacementNumber = 0;

            Random random = new Random();

            int firstRank = 10;
            int secondRank = 10;
            int[,] numbers = new int[firstRank, secondRank];

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(minRandomNumber, maxRandomNumber + 1);
                    Console.Write(numbers[i, j] + "\t");
                }

                Console.WriteLine();
            }

            foreach (int number in numbers)
            {
                if (number > maxElement)
                {
                    maxElement = number;
                }
            }

            Console.WriteLine();

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == maxElement)
                    {
                        numbers[i, j] = replacementNumber;
                    }
                }
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write(numbers[i, j] + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nМаксимальный элемент - {maxElement}");
            Console.ReadKey();
        }
    }
}
