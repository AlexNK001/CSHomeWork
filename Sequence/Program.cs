using System;

namespace Sequence
{
    internal class Program
    {
        private static void Main()
        {
            int firstNumber = 5;
            int addNumber = 7;
            int lastNumbers = 96;

            for (int numberInOutput = firstNumber; numberInOutput <= lastNumbers; numberInOutput += addNumber)
                Console.WriteLine(numberInOutput);

            Console.ReadKey();
        }
    }
}
