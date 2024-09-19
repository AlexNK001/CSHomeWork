using System;

namespace Exit_control
{
    internal class Program
    {
        private static void Main()
        {
            string exitWord = "exit";
            string userInput = string.Empty;

            while (userInput != exitWord)
                userInput = Console.ReadLine();
        }
    }
}
