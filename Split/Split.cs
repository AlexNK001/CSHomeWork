using System;

namespace Split
{
    internal class Split
    {
        private static void Main()
        {
            string text = "Что в России на первом    ? , ,,, месте, во Франции на втором, " +
                "а в Германии на третьем?";
            char[] charsToSplit = { ',', ' ', '?' };
            string[] textArray = text.Split(charsToSplit, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in textArray)
                Console.WriteLine(item);
        }
    }
}
