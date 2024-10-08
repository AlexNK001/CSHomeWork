using System;

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

            string[] names = new string[0];
            string[] professions = new string[0];
            string[] blockedMenu = { MenuWithdrawAllDossiers, MenuDeleteDossier, MenuSearchByName };
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{MenuAddDosser} - добавить досье.");
                Console.WriteLine($"{MenuWithdrawAllDossiers} - вывести все досье.");
                Console.WriteLine($"{MenuDeleteDossier} - удалить досье.");
                Console.WriteLine($"{MenuSearchByName} - поиск по фамилии.");
                Console.WriteLine($"{MenuExit} - выход.");

                string userInput = Console.ReadLine();

                bool isEmptyArray = CheckingEmptyArray(names);
                userInput = BlockedUserInput(userInput, blockedMenu, isEmptyArray);

                switch (userInput)
                {
                    case MenuAddDosser:
                        AddDosser(ref names, ref professions);
                        break;

                    case MenuWithdrawAllDossiers:
                        WithdrawAllDossiers(names, professions);
                        Console.ReadKey();
                        break;

                    case MenuDeleteDossier:
                        DeleteDossier(ref names, ref professions);
                        break;

                    case MenuSearchByName:
                        SearchByName(names, professions);
                        Console.ReadKey();
                        break;

                    case MenuExit:
                        isWork = false;
                        break;

                    default:
                        break;
                }

                Console.Clear();
            }
        }

        static void AddDosser(ref string[] names, ref string[] professions)
        {
            Console.WriteLine("Введите Ф.И.О.");
            string name = Console.ReadLine();
            Console.WriteLine("Введите должность");
            string profession = Console.ReadLine();

            if (name != "" && profession != "")
            {
                names = AddInfo(names, name);
                professions = AddInfo(professions, profession);
            }
            else
            {
                WithdrawError();
            }
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
            bool isProgramm = true;
            int menuDelete = 1;
            int menuCancellation = 0;

            while (isProgramm)
            {
                Console.Clear();

                Console.WriteLine($"Введите номер досье которое хотите удалить:");
                Console.WriteLine($"{menuCancellation} - Выход из меню удаления досье.");

                WithdrawAllDossiers(names, professions);

                int firstUserInput = Convert.ToInt32(Console.ReadLine());

                if (firstUserInput > 0 && firstUserInput <= names.Length)
                {
                    firstUserInput--;

                    WithdrawOneDossier(firstUserInput, names, professions);
                    Console.WriteLine($"Удалить данное досье? {menuDelete} - Удалить. {menuCancellation} - Отмена.");
                    int secondUserInput = Convert.ToInt32(Console.ReadLine());

                    if (secondUserInput == menuDelete)
                    {
                        names = DeleteElement(firstUserInput, names);
                        professions = DeleteElement(firstUserInput, professions);
                    }
                    else if (secondUserInput == menuCancellation)
                    {
                        Console.WriteLine("Удаление отменено.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    WithdrawError("Неверный номер досье.");
                }

                if (names.Length == 0 || firstUserInput == menuCancellation)
                    isProgramm = false;
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

        static void WithdrawError(string message = "", bool canReadKey = false)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка:{message}");
            Console.ForegroundColor = defaultColor;

            if (canReadKey == false)
                Console.ReadKey();
        }

        static void SearchByName(string[] names, string[] professions)
        {
            bool isNameFound = false;

            Console.WriteLine("Введите имя для поиска:");
            string text = Console.ReadLine();

            for (int i = 0; i < names.Length; i++)
            {
                string[] tempArray = names[i].Split();

                text = text.ToLower();
                tempArray[0] = tempArray[0].ToLower();

                if (text == tempArray[0])
                {
                    isNameFound = true;
                    WithdrawOneDossier(i, names, professions);
                }
            }

            if (isNameFound == false)
                WithdrawError("Такого имени не найдено!", true);
        }

        static bool CheckingEmptyArray(string[] array)
        {
            bool isEmptyArray = false;

            if (array.Length == 0)
                isEmptyArray = true;

            return isEmptyArray;
        }

        static string BlockedUserInput(string userInput, string[] blockedMenu, bool isBlocked)
        {
            if (isBlocked)
            {
                for (int i = 0; i < blockedMenu.Length; i++)
                {
                    if (userInput == blockedMenu[i])
                    {
                        userInput = null;
                        WithdrawError("Не найдено ни одгого досье.");
                    }
                }
            }

            return userInput;
        }
    }
}
