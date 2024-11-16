using System;
using System.Threading;

namespace Brave_new_world
{
    internal class Program
    {
        static void Main()
        {
            const ConsoleKey CommandExit = ConsoleKey.Escape;
            const int FrameDuration = 200;

            bool isPlaying = true;
            char playerSymbol = '@';
            char wallSymbol = '#';
            char voidSymbol = ' ';
            char[,] map = {
            { wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol },
            { wallSymbol, playerSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, voidSymbol, wallSymbol },
            { wallSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, voidSymbol, wallSymbol },
            { wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol, wallSymbol }};

            Console.CursorVisible = false;

            DrawMap(map);
            FindPlayer(map, out int verticalPlayerPosition, out int horizontalPlayerPosition, playerSymbol);

            while (isPlaying)
            {
                Console.SetCursorPosition(0, map.GetLength(0));
                Console.WriteLine($"Для выхода нажмите {CommandExit}");
                Console.SetCursorPosition(0, 0);

                ConsoleKey key = Console.ReadKey(true).Key;

                isPlaying = key != CommandExit;

                GetNextPlayerPosition(
                    verticalPlayerPosition,
                    horizontalPlayerPosition,
                    out int nextVerticalPlayerPosition,
                    out int nextHorizontalPlayerPosition,
                    key);

                if (CanMove(nextVerticalPlayerPosition, nextHorizontalPlayerPosition, map, wallSymbol))
                {
                    DrawOldSymbol(verticalPlayerPosition, horizontalPlayerPosition, map, wallSymbol, voidSymbol);

                    Move(ref verticalPlayerPosition,
                         ref horizontalPlayerPosition,
                         nextVerticalPlayerPosition,
                         nextHorizontalPlayerPosition);

                    DrawPlayer(verticalPlayerPosition, horizontalPlayerPosition, playerSymbol);
                }

                Thread.Sleep(FrameDuration);
            }
        }

        private static bool CanMove(int nextVerticalPlayerPosition, int nextHorizontalPlayerDirection, char[,] map, char wallSymbol)
        {
            bool canVerticalMove = nextVerticalPlayerPosition < map.GetLength(0) && nextVerticalPlayerPosition >= 0;
            bool canHorizontalMove = nextHorizontalPlayerDirection < map.GetLength(1) && nextHorizontalPlayerDirection >= 0;

            if (canVerticalMove && canHorizontalMove)
            {
                return map[nextVerticalPlayerPosition, nextHorizontalPlayerDirection] != wallSymbol;
            }
            else
            {
                return false;
            }
        }

        private static void GetNextPlayerPosition(
            int currentVerticalPlayerPosition,
            int currentHorizontalPlayerPosition,
            out int nextVerticalPlayerPosition,
            out int nextHorizontalPlayerPosition,
            ConsoleKey key)
        {
            const ConsoleKey UpArrowPressed = ConsoleKey.UpArrow;
            const ConsoleKey DownArrowPressed = ConsoleKey.DownArrow;
            const ConsoleKey LeftArrowPressed = ConsoleKey.LeftArrow;
            const ConsoleKey RightArrowPressed = ConsoleKey.RightArrow;

            nextVerticalPlayerPosition = currentVerticalPlayerPosition;
            nextHorizontalPlayerPosition = currentHorizontalPlayerPosition;

            switch (key)
            {
                case UpArrowPressed:
                    nextVerticalPlayerPosition = --currentVerticalPlayerPosition;
                    break;

                case DownArrowPressed:
                    nextVerticalPlayerPosition = ++currentVerticalPlayerPosition;
                    break;

                case LeftArrowPressed:
                    nextHorizontalPlayerPosition = --currentHorizontalPlayerPosition;
                    break;

                case RightArrowPressed:
                    nextHorizontalPlayerPosition = ++currentHorizontalPlayerPosition;
                    break;
            }
        }

        static void Move(
            ref int nextVerticalPlayerPosition,
            ref int nextHorizontalPlayerPosition,
            int verticalDirection,
            int horizontalDirection)
        {
            nextVerticalPlayerPosition = verticalDirection;
            nextHorizontalPlayerPosition = horizontalDirection;
        }

        static void DrawPlayer(
            int verticalPlayerPosition,
            int horizontalPlayerPosition,
            char playerSymbol)
        {
            Console.SetCursorPosition(horizontalPlayerPosition, verticalPlayerPosition);
            Console.Write(playerSymbol);
        }

        static void DrawOldSymbol(
            int playerX,
            int playerY,
            char[,] map,
            char symbolWall,
            char symbolVoid)
        {
            Console.SetCursorPosition(playerY, playerX);

            if (map[playerX, playerY] == symbolWall)
                Console.Write(symbolWall);
            else
                Console.Write(symbolVoid);
        }

        static void DrawMap(char[,] map)
        {
            Console.Clear();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        static void FindPlayer(char[,] map, out int playerX, out int playerY, char symbolPlayer)
        {
            playerX = 0;
            playerY = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == symbolPlayer)
                    {
                        playerX = i;
                        playerY = j;
                    }
                }
            }
        }
    }
}