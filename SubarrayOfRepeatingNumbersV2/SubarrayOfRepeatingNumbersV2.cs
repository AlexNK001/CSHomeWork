using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubarrayOfRepeatingNumbersV2
{
    internal class SubarrayOfRepeatingNumbersV2
    {
        private static void Main()
        {
            Random random = new Random();
            int minRandom = 0;
            int maxRandom = 4;
            int[] numbers = new int[30];
            int numberRepetitions = 0;

            int maxNumber = 0;
            int currentNumberRepetitions = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandom, maxRandom + 1);
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

            Console.WriteLine($"число {maxNumber} количество повторов {numberRepetitions + 1}");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
            }

            Console.ReadKey();
        }
    }
}
