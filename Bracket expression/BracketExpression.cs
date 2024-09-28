using System;

namespace Bracket_expression
{
    internal class BracketExpression
    {
        private static void Main()
        {
            char symbolLeftBrace = '(';
            char symbolRightBrace = ')';
            int nestingDepth = 0;
            int currentNestingDepth = 0;
            string braceString;

            Console.WriteLine("Введите скобочное выражение");
            braceString = Console.ReadLine();

            for (int i = 0; i < braceString.Length; i++)
            {
                if (braceString[i] == symbolRightBrace)
                    currentNestingDepth--;
                else if (braceString[i] == symbolLeftBrace)
                    currentNestingDepth++;

                if (currentNestingDepth < 0)
                    break;

                if (currentNestingDepth > nestingDepth)
                    nestingDepth = currentNestingDepth;
            }

            if (currentNestingDepth == 0)
            {
                Console.WriteLine("Скобочное выражение является корректным");
                Console.WriteLine($"Глубина вложенности {nestingDepth}");
            }
            else
            {
                Console.WriteLine("Скобочное выражение является не корректным");
            }

            Console.ReadKey();
        }
    }
}