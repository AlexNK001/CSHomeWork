using System;

namespace Dynamic_array
{
    internal class DynamicArray
    {
        private static void Main()
        {
            string inputSum = "sum";
            string inpurExit = "exit";
            string userInput;
            bool isProgramm = true;
            int[] numbers = new int[0];

            while (isProgramm)
            {
                userInput = Console.ReadLine();

                if (userInput == inputSum)
                {
                    int sum = 0;

                    for (int i = 0; i < numbers.Length; i++)
                        sum += numbers[i];

                    Console.WriteLine(sum);
                }
                else if (userInput == inpurExit)
                {
                    isProgramm = false;
                }
                else
                {
                    int[] tempNumber = new int[numbers.Length + 1];

                    for (int i = 0; i < numbers.Length; i++)
                        tempNumber[i] = numbers[i];

                    numbers = tempNumber;
                    numbers[numbers.Length - 1] = Convert.ToInt32(userInput);
                }
            }
        }
    }
}