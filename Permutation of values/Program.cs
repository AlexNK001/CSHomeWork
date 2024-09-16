using System;

namespace Permutation_of_values
{
    class Program
    {
        static void Main()
        {
            int firstNumber = 3;
            int secondNumber = 5;
            int temporaryNumber;

            Console.WriteLine($"Первое число - {firstNumber}, второе число - {secondNumber}");

            temporaryNumber = firstNumber;
            firstNumber = secondNumber;
            secondNumber = temporaryNumber;

            Console.WriteLine($"Первое число - {firstNumber}, второе число - {secondNumber}");
            Console.ReadKey();
        }
    }
}
