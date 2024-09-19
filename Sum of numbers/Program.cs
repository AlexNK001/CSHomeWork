using System;

namespace Sum_of_numbers
{
    internal class Program
    {
        private static void Main()
        {
            Random randomNumber = new Random();
            int maxValueRandom = 101;
            int number = randomNumber.Next(maxValueRandom);
            int sum = 0;
            int firstDivisor = 3;
            int secondDivisor = 5;

            for (int i = 0; i <= number; i++)
            {
                if (i % firstDivisor == 0 || i % secondDivisor == 0)
                    sum += i;
            }

            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
