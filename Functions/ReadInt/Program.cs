using System;

namespace ReadInt
{
    internal class Program
    {
        static void Main()
        {
            int number = TryRequestNumber();
            Console.WriteLine($"Выше число - {number}");
        }

        static int TryRequestNumber()
        {
            int result;
            Console.WriteLine("Введите число: ");

            while (int.TryParse(Console.ReadLine(), out result) == false)
                Console.WriteLine("Неверно, попробуйте еще раз.");

            return result;
        }
    }
}
