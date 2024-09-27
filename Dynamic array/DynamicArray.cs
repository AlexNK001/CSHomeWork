using System;

namespace Dynamic_array
{
    internal class DynamicArray
    {
        private const string InputSum = "sum";
        private const string InputExit = "exit";

        private static void Main()
        {
            char separatorsymbol = ',';
            char finalsymbol = ';';
            string userInput;
            bool isWorking = true;
            int[] numbers = new int[0];
            int sum = 0;

            while (isWorking)
            {
                Console.Clear();
                Console.WriteLine($"Для вывода суммы напишите: {InputSum}");
                Console.WriteLine($"Для выхода из программы напишите: {InputExit}");
                Console.WriteLine("Введите целочисленные числа.");

                if (numbers.Length > 0)
                {
                    string message = "";

                    for (int i = 0; i < numbers.Length - 1; i++)
                    {
                        message += numbers[i].ToString() + separatorsymbol;
                    }

                    message += numbers[numbers.Length - 1].ToString() + finalsymbol;
                    Console.WriteLine($"Ранее введенные числа: {message}");
                }

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case InputSum:
                        for (int i = 0; i < numbers.Length; i++)
                            sum += numbers[i];

                        Console.WriteLine($"Сумма чисел равна: {sum}");
                        Console.ReadKey();
                        break;

                    case InputExit:
                        isWorking = false;
                        break;

                    default:
                        int[] tempNumbers = new int[numbers.Length + 1];

                        for (int i = 0; i < numbers.Length; i++)
                            tempNumbers[i] = numbers[i];

                        numbers = tempNumbers;
                        numbers[numbers.Length - 1] = Convert.ToInt32(userInput);
                        break;
                }
            }
        }
    }
}