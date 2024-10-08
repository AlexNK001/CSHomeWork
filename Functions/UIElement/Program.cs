using System;

namespace UIElement
{
    internal class Program
    {
        static void Main()
        {
            int barLength = 18;
            float currentHealth = 40f;
            float currentMana = 54f;

            string health = GetBar(barLength, currentHealth);
            Console.WriteLine(health);
            string mana = GetBar(barLength, currentMana);
            Console.WriteLine(mana);
        }

        static string GetBar(int barLength, float currentPercent)
        {
            char startSymbol = '[';
            char presenceSymbol = '#';
            char absenceSymbol = '_';
            char endSymbol = ']';
            float maxPercent = 100f;

            string bar = string.Empty;
            int numberSymbolAvailable = Convert.ToInt32(barLength / maxPercent * currentPercent);

            for (int i = 0; i < barLength; i++)
            {
                if (i < numberSymbolAvailable)
                    bar += presenceSymbol;
                else
                    bar += absenceSymbol;
            }

            return $"{startSymbol}{bar}{endSymbol}";
        }
    }
}
