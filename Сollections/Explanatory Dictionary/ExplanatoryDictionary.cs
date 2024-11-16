using System;
using System.Collections.Generic;

namespace Explanatory_Dictionary
{
    class ExplanatoryDictionary
    {
        static void Main()
        {
            Dictionary<string, string> words = new Dictionary<string, string>();
            FillDictionary(words);
            bool isAppRunning = true;
            string commandExit = "0";

            while (isAppRunning)
            {
                Console.WriteLine($"Введите {commandExit} для выхода.");
                Console.Write("Введите слово:");
                string userInput = Console.ReadLine();

                if (userInput == commandExit)
                    isAppRunning = false;
                else if (words.ContainsKey(userInput))
                    Console.WriteLine(words[userInput]);
                else
                    Console.WriteLine("Слово не найдено");

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void FillDictionary(Dictionary<string, string> dictionary)
        {
            dictionary.Add("йога", "Система физических упражнений, выработанная последователями этого учения. Заниматься йогой.");
            dictionary.Add("лавина", "Массы снега, снежных глыб, низвергающихся с гор.");
            dictionary.Add("павлин", "павлин, павл′ин, -а, м. Птица сем. фазановых с нарядным оперением надхвостья у самцов." +
                "\nприл. ~ий, -ья, -ье. Ворона в ~ьих перьях (о том, кто хочет казаться важнее и значительнее, чем он есть на самом деле).");
            dictionary.Add("лаваш", "лаваш, лав′аш, -а, м. На юге, в Средней Азии: белый хлеб в виде большой плоской лепёшки.\nприл. ~ный, -ая, -ое.");
        }
    }
}
