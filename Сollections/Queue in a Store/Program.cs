using System;
using System.Collections.Generic;

namespace Queue_in_a_Store
{
    class Program
    {
        static void Main()
        {

            int cashAccount = 0;
            Queue<int> clients = new Queue<int>();
            string stopProgrammWord = "0";
            bool isProgramm = true;

            FillQueue(clients);

            while (isProgramm)
            {
                Console.WriteLine($"Количество денег в кассе - {cashAccount}");
                Console.WriteLine($"Введите - {stopProgrammWord}, для выхода. Любую другую клавишу для продолжения");

                string userInput = Console.ReadLine();

                if (clients.Count == 0 || userInput == stopProgrammWord)
                    isProgramm = false;
                else
                    cashAccount += clients.Dequeue();

                Console.Clear();
            }

            Console.WriteLine("Очередь кончилась.");

            Console.ReadKey();
        }

        static void FillQueue(Queue<int> clients)
        {
            int countClients = 10;
            int minRandom = 100;
            int maxRandom = 1000;
            Random random = new Random();

            for (int i = 0; i < countClients; i++)
                clients.Enqueue(random.Next(minRandom, maxRandom));
        }
    }
}
