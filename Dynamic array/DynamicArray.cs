using System;

namespace Dynamic_array
{
    internal class DynamicArray
    {
        private const string InputSum = "sum";
        private const string InpurExit = "exit";

        private static void Main()
        {
            string userInput;
            bool isWorking = true;
            int[] numbers = new int[0];
            int sum = 0;

            Console.WriteLine($"Для вывода суммы напишите: {InputSum}");
            Console.WriteLine($"Для выхода из программы напишите: {InpurExit}");
            Console.WriteLine("Введите целочисленные числа.");

            while (isWorking)
            {
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case InputSum:
                        for (int i = 0; i < numbers.Length; i++)
                            sum += numbers[i];

                        Console.WriteLine($"Сумма чисел равна: {sum}");
                        sum = 0;
                        numbers = new int[0];
                        break;

                    case InpurExit:
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