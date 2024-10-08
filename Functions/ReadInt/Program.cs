using System;

namespace ReadInt
{
    internal class Program
    {
        static void Main()
        {
            ReadInt();
        }

        static void ReadInt()
        {
            bool isWork = true;

            while (isWork)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int result))
                {
                    Console.WriteLine(result);
                    isWork = false;
                }
            }
        }
    }
}
