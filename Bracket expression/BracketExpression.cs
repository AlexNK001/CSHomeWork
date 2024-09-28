using System;

namespace Bracket_expression
{
    internal class BracketExpression
    {
        private static void Main()
        {
            char openedBracket = '(';
            char closedBracket = ')';
            int nestingDepth = 0;
            int currentNestingDepth = 0;
            string bracket;

            Console.WriteLine("Введите скобочное выражение");
            bracket = Console.ReadLine();

            for (int i = 0; i < bracket.Length; i++)
            {
                if (bracket[i] == closedBracket)
                {
                    currentNestingDepth--;

                    if (currentNestingDepth < 0)
                        break;
                }
                else if (bracket[i] == openedBracket)
                {
                    currentNestingDepth++;

                    if (currentNestingDepth > nestingDepth)
                        nestingDepth = currentNestingDepth;
                }
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