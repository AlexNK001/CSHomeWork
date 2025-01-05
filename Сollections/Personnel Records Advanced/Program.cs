using System;
using System.Collections.Generic;

namespace Personnel_Records_Advanced
{
    class Program
    {
        static void Main()
        {
            const string CommandAddDosser = "1";
            const string CommandShowAllDossiers = "2";
            const string CommandDeleteDossier = "3";
            const string CommandExit = "4";

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
                Console.WriteLine($"{CommandAddDosser} - добавить досье.");
                Console.WriteLine($"{CommandShowAllDossiers} - вывести все досье.");
                Console.WriteLine($"{CommandDeleteDossier} - удалить досье.");
                Console.WriteLine($"{CommandExit} - выход.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddDosser:
                        AddDosser(newDossiers);
                        break;

                    case CommandShowAllDossiers:
                        ShowAllDossiers(newDossiers);
                        break;

                    case CommandDeleteDossier:
                        DeleteDossier(newDossiers);
                        break;

                    case CommandExit:
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

            if (dossiers.ContainsKey(profession) == false)
            {
                List<string> names = new List<string>();
                dossiers.Add(profession, names);
            }

            dossiers[profession].Add(fullName);
        }

        private static void ShowAllDossiers(Dictionary<string, List<string>> dossiers)
        {
            Console.WriteLine($"Количество должностей {dossiers.Count}");

            foreach (string profession in dossiers.Keys)
            {
                Console.WriteLine(profession);
                ShowCollection(dossiers[profession]);
                Console.WriteLine();
            }
        }

        private static void DeleteDossier(Dictionary<string, List<string>> dossier)
        {
            Console.WriteLine("Выберите профессию");
            ShowCollection(dossier.Keys);

            if (TrySelectProffesion(dossier, out string proffesion))
            {
                List<string> names = dossier[proffesion];
                ShowCollection(names);

                if (TryGetIndex(names.Count, out int result))
                {
                    Console.WriteLine($"{proffesion} {names[result]} - успешно удалён");
                    names.RemoveAt(result);

                    if (names.Count == 0)
                    {
                        dossier.Remove(proffesion);
                    }
                }
                else
                {
                    Console.WriteLine("Неверно выбран номер проффесии");
                }
            }
            else
            {
                Console.WriteLine("Неверно выбрана проффесия");
            }
        }

        private static string ReadData(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private static void ShowCollection(IEnumerable<string> names)
        {
            int number = 1;

            foreach (string name in names)
            {
                Console.WriteLine($"{number} - {name}");
                number++;
            }
        }

        private static bool TrySelectProffesion(Dictionary<string, List<string>> dossier, out string proffesion)
        {
            proffesion = string.Empty;

            if (TryGetIndex(dossier.Count, out int index))
            {
                int currentIndex = 0;

                foreach (string currentProffesion in dossier.Keys)
                {
                    if (currentIndex == index)
                    {
                        proffesion = currentProffesion;
                        return true;
                    }

                    currentIndex++;
                }
            }

            return false;
        }

        private static bool TryGetIndex(int count, out int result)
        {
            if (int.TryParse(Console.ReadLine(), out result))
            {
                result--;
                return result >= 0 && result < count;
            }

            return false;
        }
    }
}