﻿using System;

namespace Personnel_Records
{
    internal class Program
    {
        static void Main()
        {
            const string MenuAddDosser = "1";
            const string MenuWithdrawAllDossiers = "2";
            const string MenuDeleteDossier = "3";
            const string MenuSearchByName = "4";
            const string MenuExit = "5";

            string[] names = new string[] 
            { 
                "Козлов Петр Алексеевич", 
                "Иванов Иван Иваныч", 
                "Иванов Лёша Иваныч", 
                "Иванов Федя Иваныч" 
            };

            string[] professions = new string[] 
            { 
                "столяр", 
                "маляр", 
                "каменьщик", 
                "водитель" 
            };

            bool isProgramm = true;

            while (isProgramm)
            {
                Console.WriteLine($"{MenuAddDosser} - добавить досье.");
                Console.WriteLine($"{MenuWithdrawAllDossiers} - вывести все досье.");
                Console.WriteLine($"{MenuDeleteDossier} - удалить досье.");
                Console.WriteLine($"{MenuSearchByName} - поиск по фамилии.");
                Console.WriteLine($"{MenuExit} - выход.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuAddDosser:
                        AddDossier(ref names, ref professions);
                        break;

                    case MenuWithdrawAllDossiers:
                        WithdrawAllDossiers(names, professions);
                        break;

                    case MenuDeleteDossier:
                        DeleteDossier(ref names, ref professions);
                        break;

                    case MenuSearchByName:
                        SearchByLastName(names, professions);
                        break;

                    case MenuExit:
                        isProgramm = false;
                        break;

                    default:
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void AddDossier(ref string[] names, ref string[] professions)
        {
            if (TryReadText("Введите Ф.И.О.", out string first))
            {
                if (TryReadText("Введите должность", out string second))
                {
                    names = AddInfo(names, first);
                    professions = AddInfo(professions, second);
                }
                else
                {
                    WithdrawError("Некорректная должность");
                    Console.ReadKey();
                }
            }
            else
            {
                WithdrawError("Некорректное Ф.И.О.");
                Console.ReadKey();
            }
        }

        static bool TryReadText(string message, out string text)
        {
            Console.Write(message);
            text = Console.ReadLine();
            return text != string.Empty;
        }

        static bool TryReadIndex(string message, string[] names, out int number)
        {
            Console.Write(message);
            bool isNumber = int.TryParse(Console.ReadLine(), out number);
            bool isIndex = number > 0 && number <= names.Length;
            return isNumber && isIndex;
        }

        static string[] AddInfo(string[] array, string info)
        {
            string[] tempArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                tempArray[i] = array[i];

            array = tempArray;
            array[array.Length - 1] = info;

            return array;
        }

        static void WithdrawAllDossiers(string[] names, string[] professions)
        {
            Console.WriteLine("Все досье:");

            for (int i = 0; i < names.Length; i++)
                WithdrawOneDossier(i, names, professions);
        }

        static void WithdrawOneDossier(int indexDossier, string[] names, string[] professions)
        {
            int numberDeleteDossier = indexDossier + 1;

            Console.WriteLine($"{numberDeleteDossier} - {names[indexDossier]} - {professions[indexDossier]}");
        }

        static void DeleteDossier(ref string[] names, ref string[] professions)
        {
            Console.Clear();

            WithdrawAllDossiers(names, professions);

            if (TryReadIndex("Введите номер досье которое хотите удалить:", names, out int index))
            {
                index--;
                names = DeleteElement(index, names);
                professions = DeleteElement(index, professions);
            }
            else
            {
                WithdrawError("Неверный номер досье.");
            }
        }

        static string[] DeleteElement(int numberElement, string[] array)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < numberElement; i++)
                tempArray[i] = array[i];

            for (int i = numberElement; i < tempArray.Length; i++)
                tempArray[i] = array[i + 1];

            array = tempArray;

            return array;
        }

        static void WithdrawError(string message)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка:{message}");
            Console.ForegroundColor = defaultColor;
        }

        static void SearchByLastName(string[] names, string[] professions)
        {
            if (TryReadText("Введите фамилию для поиска:", out string lastName))
            {
                for (int i = 0; i < names.Length; i++)
                {
                    string[] fullName = names[i].Split();

                    if (lastName.ToLower() == fullName[0].ToLower())
                    {
                        WithdrawOneDossier(i, names, professions);
                    }
                }
            }
            else
            {
                WithdrawError("Некорректная фамилия для поиска.");
            }
        }
    }
}