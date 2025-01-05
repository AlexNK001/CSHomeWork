using System;
using System.Collections.Generic;

namespace CSLight_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] firstNumbers = FillArray(14);
            int[] secondNumbers = FillArray(18);

            List<int> numbers = new List<int>();

            WithDrawArray(firstNumbers, "Первый массив");
            WithDrawArray(secondNumbers, "Второй массив");

            Merging(firstNumbers, ref numbers);
            Merging(secondNumbers, ref numbers);

            Console.WriteLine("Лист");

            foreach (int number in numbers)
                Console.Write($"{number} ");

            Console.ReadKey();
        }

        static int[] FillArray(int length, int minRandom = 0, int maxRandom = 15)
        {
            int[] array = new int[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
                array[i] = random.Next(minRandom, maxRandom);

            return array;
        }

        static void Merging(int[] array, ref List<int> list)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!list.Contains(array[i]))
                    list.Add(array[i]);
            }
        }

        static void WithDrawArray(int[] array, string message)
        {
            Console.WriteLine(message);

            foreach (int number in array)
                Console.Write($"{number} ");

            Console.WriteLine();
        }
    }
}
