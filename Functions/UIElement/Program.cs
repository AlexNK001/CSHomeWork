using System;

namespace UIElement
{
    internal class Program
    {
        static void Main()
        {
            int barLength = 18;
            int maxValueForHealth = 100;
            int currentHealth = 18;
            int maxValueForMana = 70;
            int currentMana = 54;

            DrawFullBar(barLength, maxValueForHealth, currentHealth);
            DrawFullBar(barLength, maxValueForMana, currentMana, ConsoleColor.DarkBlue);
        }

        static void DrawFullBar(int barSizeInSymbol, int maximumPossibleValue, int presentValue, ConsoleColor color = ConsoleColor.DarkRed)
        {
            float percent = Convert.ToSingle(presentValue) / maximumPossibleValue;
            int firstHalfBar = Convert.ToInt32(barSizeInSymbol * percent);
            int secondHalfBar = barSizeInSymbol - firstHalfBar;

            Console.Write('[');
            ConsoleColor defaultColor = Console.BackgroundColor;
            Console.BackgroundColor = color;

            DrawHalfBar(firstHalfBar, '#');

            Console.BackgroundColor = defaultColor;

            DrawHalfBar(secondHalfBar, '_');

            Console.WriteLine(']');
        }

        static void DrawHalfBar(int numberSymbol, char symbolInBar)
        {
            for (int i = 0; i < numberSymbol; i++)
                Console.Write(symbolInBar);
        }
    }
}
