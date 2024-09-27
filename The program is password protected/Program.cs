using System;

namespace The_program_is_password_protected
{
    internal class Program
    {
        private static void Main()
        {
            string secretMessage = "Desine sperare qui hic intras";
            string password = "qwerty";
            string userInput;
            int numberAttempts = 3;

            for (int i = numberAttempts; i > 0; i--)
            {
                Console.WriteLine($"Колличество попыток - {i}");
                Console.Write("Для получения секретной фразы, введите пароль:");
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine(secretMessage);
                    Console.ReadKey();
                    break;
                }
            }
        }
    }
}