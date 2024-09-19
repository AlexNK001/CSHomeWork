using System;

namespace Console_menu
{
    internal class Program
    {
        private static void Main()
        {
            const string MenuCharactersBetweenLines = "1";
            const string MenuNumberTelephone = "2";
            const string MenuPassword = "3";
            const string ComandLineColor = "4";
            const string ComandExitProgram = "5";

            const string NumberColorRed = "1";
            const string NumberColorGreen = "2";
            const string NumberColorBlue = "3";
            const string NumberColorMagenta = "4";
            const string NumberColorDarkYellow = "5";
            const string NumberColorDefault = "7";

            string numberColorRandom = "6";

            string betweenLines = null;
            string numberComandInputFirstCharacter = "1";
            string numberComandInputSecondCharacter = "2";
            string numberComandDeleteAllBetweenLines = "3";
            char firstChar = '\0';
            char secondChar = '\0';
            char defaultChar = '\0';
            int numberCharactersBetweenLines = 25;

            bool isTelephoneNumberEntered = false;
            string telephoneNumber = null;
            string userInputNuberTelephone;
            int countSymbolNumberTelephe = 11;

            bool isTelephonePasswordEntered = false;
            string telephonePassword = null;
            string firstInputPassword;
            string secondInputPassword;

            int minCountRandomColor = 1;
            int maxCountRandomColor = 6;

            bool isProgram = true;
            string error = "Ошибка!";

            while (isProgram)
            {
                bool isNoPasswordNoNumberTelephoneEntered = isTelephoneNumberEntered == false && isTelephonePasswordEntered == false;
                bool isNoPasswordNumberTelephoneEntered = isTelephoneNumberEntered == true && isTelephonePasswordEntered == false;
                bool isPasswordNumberTelephoneEntered = isTelephoneNumberEntered == true && isTelephonePasswordEntered == true;

                Console.Write(betweenLines);
                Console.WriteLine($"{MenuCharactersBetweenLines} - Разделители между строк");
                Console.Write(betweenLines);

                if (isTelephoneNumberEntered)
                {
                    if (isTelephonePasswordEntered)
                    {
                        Console.WriteLine($"{MenuNumberTelephone} - Ввести пароль для открытия номера телефона");
                        Console.WriteLine("\tНомер телефона скрыт");
                    }
                    else
                    {
                        Console.WriteLine($"{MenuNumberTelephone} - Сменить номер телефона");
                        Console.WriteLine($"\tВаш номер телефона - {telephoneNumber}");
                    }
                }
                else
                {
                    Console.WriteLine($"{MenuNumberTelephone} - Указать номер телефона");
                }

                Console.Write(betweenLines);

                if (isTelephonePasswordEntered)
                {
                    Console.WriteLine($"{MenuPassword} - Удалить пароль скрывающий номер телефона");
                }
                else
                {
                    Console.WriteLine($"{MenuPassword} - Установить пароль скрывающий номер телефона");
                }

                Console.Write(betweenLines);
                Console.WriteLine($"{ComandLineColor} - Выбрать цвет меню");
                Console.Write(betweenLines);
                Console.WriteLine($"{ComandExitProgram} - Выход из программы");
                Console.Write(betweenLines);
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuCharactersBetweenLines:
                        Console.WriteLine($"{numberComandInputFirstCharacter} - Ввести символ");
                        Console.WriteLine($"{numberComandInputSecondCharacter} - Ввести второй символ");
                        Console.WriteLine($"{numberComandDeleteAllBetweenLines} - Удалить разделители между строк");
                        string userInputChar = Console.ReadLine();

                        if (userInputChar == numberComandInputFirstCharacter || userInputChar == numberComandInputSecondCharacter)
                        {
                            betweenLines = null;

                            if (userInputChar == numberComandInputFirstCharacter)
                            {
                                Console.Write("Введите первый символ");
                                firstChar = Convert.ToChar(Console.ReadLine());

                                if (secondChar == defaultChar)
                                {
                                    secondChar = firstChar;
                                }
                            }
                            else if (userInputChar == numberComandInputSecondCharacter)
                            {
                                Console.Write("Введите второй символ");
                                secondChar = Convert.ToChar(Console.ReadLine());

                                if (firstChar == defaultChar)
                                {
                                    firstChar = secondChar;
                                }
                            }

                            for (int i = 0; i < numberCharactersBetweenLines; i++)
                            {
                                betweenLines += firstChar;
                                betweenLines += secondChar;
                            }

                            betweenLines += "\n";
                        }
                        else if (userInputChar == numberComandDeleteAllBetweenLines)
                        {
                            betweenLines = null;
                            firstChar = '\0';
                            secondChar = '\0';
                        }

                        break;

                    case MenuNumberTelephone:
                        if (isPasswordNumberTelephoneEntered)
                        {
                            Console.WriteLine($"Введите пароль, чтобы показать номер телефона");
                            firstInputPassword = Console.ReadLine();

                            if (firstInputPassword == telephonePassword)
                            {
                                Console.WriteLine($"Ваш номер телефона - {telephoneNumber}");
                            }
                            else
                            {
                                Console.WriteLine(error);
                            }

                            Console.ReadKey();
                        }
                        else if (isNoPasswordNoNumberTelephoneEntered || isNoPasswordNumberTelephoneEntered)
                        {
                            Console.WriteLine($"Номер телефона должен содержать {countSymbolNumberTelephe} символов!");
                            userInputNuberTelephone = Convert.ToString(Console.ReadLine());

                            if (userInputNuberTelephone.Length == countSymbolNumberTelephe)
                            {
                                telephoneNumber = userInputNuberTelephone;
                                isTelephoneNumberEntered = true;
                            }
                            else
                            {
                                Console.WriteLine(error);
                                Console.ReadKey();
                            }
                        }

                        break;

                    case MenuPassword:
                        if (isPasswordNumberTelephoneEntered)
                        {
                            Console.WriteLine("Введите пароль, что бы подтвердить удаление");
                            firstInputPassword = Convert.ToString(Console.ReadLine());

                            if (firstInputPassword == telephonePassword)
                            {
                                telephonePassword = null;
                                isTelephonePasswordEntered = false;
                            }
                            else
                            {
                                Console.WriteLine(error);
                                Console.ReadKey();
                            }
                        }
                        else if (isNoPasswordNumberTelephoneEntered)
                        {
                            Console.Write("Введите пароль:");
                            firstInputPassword = Convert.ToString(Console.ReadLine());

                            Console.Write("Повторите пароль:");
                            secondInputPassword = Convert.ToString(Console.ReadLine());

                            if (firstInputPassword == secondInputPassword)
                            {
                                telephonePassword = firstInputPassword;
                                isTelephonePasswordEntered = true;
                            }
                            else
                            {
                                Console.Write(error);
                                Console.ReadKey();
                            }
                        }
                        else if (isNoPasswordNoNumberTelephoneEntered)
                        {
                            Console.Write("Сначало укажите номер телефона!");
                            Console.ReadKey();
                        }

                        break;

                    case ComandLineColor:
                        Random colorMenu = new Random();
                        int randomColor = colorMenu.Next(minCountRandomColor, maxCountRandomColor);

                        Console.WriteLine($"{NumberColorRed} - Красный");
                        Console.WriteLine($"{NumberColorGreen} - Зеленый");
                        Console.WriteLine($"{NumberColorBlue} - Синий");
                        Console.WriteLine($"{NumberColorMagenta} - Фиолетовый");
                        Console.WriteLine($"{NumberColorDarkYellow} - Желтый");
                        Console.WriteLine($"{numberColorRandom} - Случайный");
                        Console.WriteLine($"{NumberColorDefault} - Стандартный");
                        string userInputNumberColor = Console.ReadLine();

                        if (userInputNumberColor == numberColorRandom)
                        {
                            userInputNumberColor = Convert.ToString(randomColor);
                        }

                        switch (userInputNumberColor)
                        {
                            case NumberColorRed:
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;

                            case NumberColorGreen:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;

                            case NumberColorBlue:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;

                            case NumberColorMagenta:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;

                            case NumberColorDarkYellow:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;

                            case NumberColorDefault:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;

                            default:
                                Console.WriteLine(error);
                                Console.ReadKey();
                                break;
                        }
                        break;

                    case ComandExitProgram:
                        isProgram = false;
                        break;

                    default:
                        Console.WriteLine(error);
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }
    }
}
