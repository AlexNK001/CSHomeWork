using System;

namespace Mastering_Cycles
{
    public class Program
    {
        static void Main()
        {
            Console.Write("Введети текст:");
            string inputUserText = Console.ReadLine();

            Console.Write("Введети количество повторов:");
            int countCycles = Convert.ToInt32(Console.ReadLine());

            for (int i = countCycles; i > 0; i--)
                Console.WriteLine(inputUserText);

            Console.ReadKey();
        }
    }
}
