using System;
using System.Collections.Generic;

namespace Personnel_Records_Advanced
{
    class Program
    {
        static void Main()
        {
            const string MenuAddDosser = "1";
            const string MenuWithdrawAllDossiers = "2";
            const string MenuDeleteDossier = "3";
            const string MenuExit = "4";

            Dictionary<string, string> dossiers = new Dictionary<string, string>();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"{MenuAddDosser} - добавить досье.");
                Console.WriteLine($"{MenuWithdrawAllDossiers} - вывести все досье.");
                Console.WriteLine($"{MenuDeleteDossier} - удалить досье.");
                Console.WriteLine($"{MenuExit} - выход.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuAddDosser:
                        AddDosser(dossiers);
                        break;

                    case MenuWithdrawAllDossiers:
                        WithdrawAllDossiers(dossiers);
                        break;

                    case MenuDeleteDossier:
                        DeleteDossier(dossiers);
                        break;

                    case MenuExit:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }

                Console.Clear();
            }
        }

        static void WithdrawDossiers(Dictionary<string, string> dossiers)
        {
            foreach (KeyValuePair<string, string> dossier in dossiers)
                Console.WriteLine($"{dossier.Key} - {dossier.Value}");
        }

        static string ReadData(out string value, string message)
        {
            Console.WriteLine(message);
            value = Console.ReadLine();

            return value;
        }


        static void AddDosser(Dictionary<string, string> dossiers)
        {
            Console.Clear();

            ReadData(out string currentName, "Введите Ф.И.О.");
            ReadData(out string currentProfession, "Введите должность.");

            if (dossiers.ContainsKey(currentName))
                Console.WriteLine("Такое имя уже есть");
            else
                dossiers.Add(currentName, currentProfession);
        }

        static void WithdrawAllDossiers(Dictionary<string, string> dossiers)
        {
            Console.Clear();

            if (dossiers.Count == 0)
                Console.WriteLine("Не одного досье ни найдено.");
            else
                WithdrawDossiers(dossiers);

            Console.ReadKey();
        }

        static void DeleteDossier(Dictionary<string, string> dossiers)
        {
            string commandExit = "0";
            bool isDelete = true;

            Console.Clear();

            while (isDelete)
            {
                if (dossiers.Count == 0)
                {
                    isDelete = false;
                    Console.WriteLine("Не одного досье ни найдено.");
                    Console.ReadKey();
                }
                else
                {
                    Console.SetCursorPosition(0, 3);

                    WithdrawDossiers(dossiers);

                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine("Введите Ф.И.О сотрудника, которого хотите удалить.");
                    Console.Write($"Введите {commandExit} для выхода из меню удаления:");
                    string userInput = Console.ReadLine();

                    if (userInput == commandExit)
                    {
                        isDelete = false;
                    }
                    else if (dossiers.ContainsKey(userInput))
                    {
                        dossiers.Remove(userInput);
                    }
                    else
                    {
                        Console.Write("Данный сотрудник не найден.");
                        Console.ReadKey();
                    }

                    Console.Clear();
                }
            }
        }
    }
}
