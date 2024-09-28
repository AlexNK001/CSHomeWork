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
            string[] words = text.Split(charsToSplit, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
                Console.WriteLine(word);
        }
    }
}
