using System;

namespace Subarray_of_repeating_numbers
{
    internal class SubarrayOfRepeatingNumbers
    {
        private static void Main()
        {
            Random random = new Random();
            int minRandom = 0;
            int maxRandom = 4;
            int[] numbers = new int[30];
            int numberRepetitions = 0;
            bool isWorking = true;
            int currentNumberRepetitions = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandom, maxRandom + 1);
            }

            while (isWorking)
            {
                isWorking = false;
                Console.Clear();

                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] == numbers[i + 1])
                    {
                        currentNumberRepetitions++;

                        if (currentNumberRepetitions > numberRepetitions)
                        {
                            isWorking = true;
                            numberRepetitions = currentNumberRepetitions;
                        }
                        else if (currentNumberRepetitions == numberRepetitions)
                        {
                            Console.WriteLine($"число {numbers[i]} количество повторов {currentNumberRepetitions + 1}");
                        }
                    }
                    else
                    {
                        currentNumberRepetitions = 0;
                    }
                }
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
            }

            Console.ReadKey();
        }
    }
}
