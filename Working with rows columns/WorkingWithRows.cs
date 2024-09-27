using System;

namespace Working_with_rows_columns
{
    internal class WorkingWithRows
    {
        private static void Main()
        {
            int minRandomNumber = 0;
            int maxRandomNumber = 101;
            int sum = 0;
            int defaultProoductNumbers;
            int productNumbers;
            int lineNumber = 1;
            int columnNumber = 0;
            int firstRank = 2;
            int secondRank = 7;
            int lineNumberIsString = lineNumber + 1;
            int columnNumberIsString = columnNumber + 1;

            Random random = new Random();
            int[,] numbers = new int[firstRank, secondRank];

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(minRandomNumber, maxRandomNumber);
                    Console.Write(numbers[i, j] + "\t");
                }

                Console.WriteLine();
            }

            for (int i = 0; i < numbers.GetLength(1); i++)
            {
                sum += numbers[lineNumber, i];
            }

            defaultProoductNumbers = numbers.GetLowerBound(0);
            productNumbers = numbers[columnNumber, defaultProoductNumbers];

            for (int i = 1; i < numbers.GetLength(0); i++)
            {
                productNumbers *= numbers[i, columnNumber];
            }

            Console.WriteLine($"\nСумма чисел строки под номером {lineNumberIsString} равна - {sum}");
            Console.WriteLine($"\nПроизведение столбца под номером {columnNumberIsString} равна - {productNumbers}");
            Console.ReadKey();
        }
    }
}
