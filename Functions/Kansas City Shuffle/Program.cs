using System;

namespace Kansas_City_Shuffle
{

    internal class Program
    {
        static void Main()
        {
            string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };

            OutputArray(numbers, "Массив до перемешивания");

            numbers = ShuffleArray(numbers);

            OutputArray(numbers, "Массив после перемешивания");

            Console.ReadKey();
        }

        static string[] ShuffleArray(string[] array)
        {
            Random random = new Random();

            string tempElement;

            for (int i = 0; i < array.Length; i++)
            {
                int index = random.Next(array.Length);
                tempElement = array[i];
                array[i] = array[index];
                array[index] = tempElement;
            }

            return array;
        }

        static void OutputArray(string[] array, string message)
        {
            Console.WriteLine(message);

            foreach (string element in array)
                Console.Write($"{element} ");

            Console.WriteLine();
        }
    }
}
