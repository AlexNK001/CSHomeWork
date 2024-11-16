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

            List<string> firstGroupNames = new List<string>()
            {
                "Витя",
                "Петя",
                "Коля",
                "Саша",
                "Паша"
            };

            List<string> secondGroupNames = new List<string>()
            {
                "Оля",
                "Толя",
                "Игорь",
                "Даша",
                "Каша"
            };

            Dictionary<string, List<string>> newDossiers = new Dictionary<string, List<string>>()
            {
                { "Водитель", firstGroupNames },
                { "Маляр", secondGroupNames },
                { "Кондитер", new List<string>{ "Женя"} }
            };

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
                        AddDosser(newDossiers);
                        break;

                    case MenuWithdrawAllDossiers:
                        WithdrawAllDossiers(newDossiers);
                        break;

                    case MenuDeleteDossier:
                        DeleteDossier(newDossiers);
                        break;

                    case MenuExit:
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddDosser(Dictionary<string, List<string>> dossiers)
        {
            string fullName = ReadData("Введите Ф.И.О.");
            string profession = ReadData("Введите должность.");
            List<string> names;

            if (dossiers.ContainsKey(profession))
            {
                names = dossiers[profession];
            }
            else
            {
                names = new List<string>();
                dossiers.Add(profession, names);
            }

            names.Add(fullName);
        }

        private static void WithdrawAllDossiers(Dictionary<string, List<string>> dossiers)
        {
            Console.WriteLine($"Количество должностей {dossiers.Count}");

            foreach (string profession in dossiers.Keys)
            {
                List<string> names = dossiers[profession];

                for (int i = 0; i < names.Count; i++)
                {
                    Console.WriteLine($"{profession} {names[i]}");
                }
            }
        }

        private static void DeleteDossier(Dictionary<string, List<string>> dossiers)
        {
            Console.Clear();

            if (TryGetName(dossiers, out string profession, out string name))
            {
                List<string> currentNames = dossiers[profession];
                currentNames.Remove(name);

                if (currentNames.Count == 0)
                {
                    dossiers.Remove(profession);
                }
            }
        }

        private static bool TryGetName(Dictionary<string, List<string>> dossier, out string profession, out string names)
        {
            profession = string.Empty;
            names = string.Empty;

            if (dossier.Count == 0)
            {
                Console.WriteLine("Не одного досье ни найдено.");
                return false;
            }

            string name = ReadData("Введите Ф.И.О сотрудника, которого хотите удалить.");

            foreach (string currentProffesion in dossier.Keys)
            {
                List<string> currentNames = dossier[currentProffesion];

                if (currentNames.Contains(name))
                {
                    profession = currentProffesion;
                    names = name;
                    return true;
                }
            }

            Console.WriteLine("Такой сотрудник не найден.");
            return false;
        }

        private static string ReadData(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
