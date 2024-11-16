using System;
using System.Collections.Generic;

namespace Dynamic_Array_Advanced
{
    class Program
    {
        static void Main()
        {
            string commandSum = "sum";
            string commandExit = "exit";
            string userInput;
            bool isProgramm = true;
            List<int> numbers = new List<int>();

            while (isProgramm)
            {
                userInput = Console.ReadLine();

                if (userInput == commandSum)
                    CalculateAmount(numbers);
                else if (userInput == commandExit)
                    isProgramm = false;
                else
                    AddNumber(userInput, numbers);
            }
        }

        static void CalculateAmount(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
                sum += number;

            Console.WriteLine(sum);
        }

        static void AddNumber(string userInput, List<int> numbers)
        {
            if (int.TryParse(userInput, out int number))
                numbers.Add(number);
            else
                Console.WriteLine("Ошибка");
        }
    }
}
