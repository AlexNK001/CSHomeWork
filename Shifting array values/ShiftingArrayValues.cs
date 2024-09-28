using System;

namespace Shifting_array_values
{
    internal class ShiftingArrayValues
    {
        private static void Main()
        {
            int[] numbers = new int[10];
            Random random = new Random();
            int minRandom = 0;
            int maxRandom = 10;
            int userInput;
            int firstIndex;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandom, maxRandom + 1);
                Console.Write($"{numbers[i]} ");
            }

            userInput = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < userInput; i++)
            {
                firstIndex = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                    numbers[j] = numbers[j + 1];

                numbers[numbers.GetUpperBound(0)] = firstIndex;
            }

            Console.WriteLine();

            foreach (int number in numbers)
                Console.Write($"{number} ");
        }
    }
}
