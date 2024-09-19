using System;

namespace Сurrency_converter
{
    internal class Program
    {
        private static void Main()
        {
            const string CommandRubToUsd = "1";
            const string CommandRubToEur = "2";
            const string CommandUsdToRub = "3";
            const string CommandUsdToEur = "4";
            const string CommandEurToRub = "5";
            const string CommandEurToUsd = "6";
            const string CommandExitProgram = "7";

            float userRub = 25000;
            float userUsd = 500;
            float userEur = 250;
            float rubToUsd = 76;
            float rubToEur = 80;
            float usdToRub = 0.0131578947368421f;
            float eurToRub = 0.0125f;
            float usdToEur = 0.95f;
            float eurToUsd = 1.052631578947368f;
            string invalodInput = "Введено неверное колличество";
            bool isInProgram = true;
            float inputExchangeValue;

            while (isInProgram)
            {
                Console.WriteLine($"Ваш баланс: {userRub} - рублей. {userUsd} - долларов. {userEur} - евро");
                Console.WriteLine($"{CommandRubToUsd} - Обменять рубли на доллары");
                Console.WriteLine($"{CommandRubToEur} - Обменять рубли на евро");
                Console.WriteLine($"{CommandUsdToRub} - Обменять доллары на рубли");
                Console.WriteLine($"{CommandUsdToEur} - Обменять доллары на евро");
                Console.WriteLine($"{CommandEurToRub} - Обменять евро на рубли");
                Console.WriteLine($"{CommandEurToUsd} - Обменять евро на доллары");
                Console.WriteLine($"{CommandExitProgram} - Выход из программы");
                string inputOperationNumber = Console.ReadLine();

                switch (inputOperationNumber)
                {
                    case CommandRubToUsd:
                        Console.Write("Введите количество рублей которое вы хотите обменять на доллары:");
                        inputExchangeValue = Convert.ToSingle(Console.ReadLine());

                        if (inputExchangeValue <= userRub)
                        {
                            userRub -= inputExchangeValue;
                            userUsd += inputExchangeValue * usdToRub;
                        }
                        else
                        {
                            Console.WriteLine(invalodInput);
                        }
                        break;

                    case CommandRubToEur:
                        Console.Write("Введите количество рублей которое вы хотите обменять на евро:");
                        inputExchangeValue = Convert.ToSingle(Console.ReadLine());

                        if (inputExchangeValue <= userRub)
                        {
                            userRub -= inputExchangeValue;
                            userEur += inputExchangeValue * eurToRub;
                        }
                        else
                        {
                            Console.WriteLine(invalodInput);
                        }
                        break;

                    case CommandUsdToRub:
                        Console.Write("Введите количество долларов которое вы хотите обменять на рубли:");
                        inputExchangeValue = Convert.ToSingle(Console.ReadLine());

                        if (inputExchangeValue <= userUsd)
                        {
                            userUsd -= inputExchangeValue;
                            userRub += inputExchangeValue * rubToUsd;
                        }
                        else
                        {
                            Console.WriteLine(invalodInput);
                        }
                        break;

                    case CommandUsdToEur:
                        Console.Write("Введите количество долларов которое вы хотите обменять на евро:");
                        inputExchangeValue = Convert.ToSingle(Console.ReadLine());

                        if (inputExchangeValue <= userUsd)
                        {
                            userUsd -= inputExchangeValue;
                            userEur += inputExchangeValue * usdToEur;
                        }
                        else
                        {
                            Console.WriteLine(invalodInput);
                        }
                        break;

                    case CommandEurToRub:
                        Console.Write("Введите количество евро которое вы хотите обменять на рубли:");
                        inputExchangeValue = Convert.ToSingle(Console.ReadLine());

                        if (inputExchangeValue <= userEur)
                        {
                            userEur -= inputExchangeValue;
                            userRub += inputExchangeValue * rubToEur;
                        }
                        else
                        {
                            Console.WriteLine(invalodInput);
                        }
                        break;

                    case CommandEurToUsd:
                        Console.Write("Введите количество евро которое вы хотите обменять на доллары:");
                        inputExchangeValue = Convert.ToSingle(Console.ReadLine());

                        if (inputExchangeValue <= userEur)
                        {
                            userEur -= inputExchangeValue;
                            userUsd += inputExchangeValue * eurToUsd;
                        }
                        else
                        {
                            Console.WriteLine(invalodInput);
                        }
                        break;

                    case CommandExitProgram:
                        isInProgram = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        Console.ReadKey();
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}