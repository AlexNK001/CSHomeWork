using System;

namespace Sorting_numbers
{
    internal class SortingNumbers
    {
        private static void Main()
        {
            Random random = new Random();
            int minRandom = 0;
            int maxRandom = 10;
            int[] numbers = new int[15];
            bool isWorking = true;
            int maxValue = int.MaxValue;
            int currentMaxValue;
            int currentMinValue = int.MinValue;
            int numberCycles = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandom, maxRandom + 1);
                Console.Write(numbers[i] + " ");
            }

            Console.WriteLine();

            while (isWorking)
            {
                currentMaxValue = maxValue;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] > currentMinValue && numbers[i] < currentMaxValue)
                        currentMaxValue = numbers[i];
                    else if (numbers[i] == currentMinValue)
                        Console.Write(numbers[i] + " ");
                }

                currentMinValue = currentMaxValue;

                numberCycles++;

                if (numberCycles == numbers.Length)
                    isWorking = false;
            }
        }
    }
}
