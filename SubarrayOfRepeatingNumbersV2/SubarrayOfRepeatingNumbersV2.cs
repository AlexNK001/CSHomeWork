using System;

namespace SubarrayOfRepeatingNumbers
{
    internal class SubarrayOfRepeatingNumbers
    {
        private static void Main()
        {
            Random random = new Random();
            int minRandom = 0;
            int maxRandom = 4;
            int[] numbers = new int[30];
            int numberRepetitions = 1;

            int maxNumber = 0;
            int currentNumberRepetitions = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandom, maxRandom + 1);
                Console.Write($"{numbers[i]} ");
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    currentNumberRepetitions++;

                    if (currentNumberRepetitions > numberRepetitions)
                    {
                        maxNumber = numbers[i];
                        numberRepetitions = currentNumberRepetitions;
                    }
                }
                else
                {
                    currentNumberRepetitions = 0;
                }
            }

            Console.WriteLine($"\nчисло {maxNumber} количество повторов {numberRepetitions}.");
            Console.ReadKey();
        }
    }
}


