using System;
using System.Threading;

namespace Brave_new_world
{
    internal class Program
    {
        const ConsoleKey UpArrowPressed = ConsoleKey.UpArrow;
        const ConsoleKey DownArrowPressed = ConsoleKey.DownArrow;
        const ConsoleKey LeftArrowPressed = ConsoleKey.LeftArrow;
        const ConsoleKey RightArrowPressed = ConsoleKey.RightArrow;
        const ConsoleKey CommandExit = ConsoleKey.Escape;

        const int FrameDuration = 200;

        static void Main()
        {
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
                int nextVerticalPlayerPosition = verticalPlayerPosition;
                int nextHorizontalPlayerPosition = horizontalPlayerPosition;

                Console.SetCursorPosition(0, map.GetLength(0));
                Console.WriteLine($"Для выхода нажмите {CommandExit}");
                Console.SetCursorPosition(0, 0);

                ConsoleKey key = Console.ReadKey(true).Key;

                isPlaying = key != CommandExit;

                GetNextPlayerPosition(ref nextVerticalPlayerPosition, ref nextHorizontalPlayerPosition, key);

                if (IsBorders(nextVerticalPlayerPosition, nextHorizontalPlayerPosition, map))
                {
                    if (map[nextVerticalPlayerPosition, nextHorizontalPlayerPosition] != wallSymbol)
                    {
                        DrawOldSymbol(verticalPlayerPosition, horizontalPlayerPosition, map, wallSymbol, voidSymbol);

                        Move(ref verticalPlayerPosition,
                             ref horizontalPlayerPosition,
                             nextVerticalPlayerPosition,
                             nextHorizontalPlayerPosition);

                        DrawPlayer(verticalPlayerPosition, horizontalPlayerPosition, playerSymbol);
                    }
                }

                Thread.Sleep(FrameDuration);
            }
        }

        private static void GetNextPlayerPosition(
            ref int nextVerticalPlayerPosition,
            ref int nextHorizontalPlayerPosition,
            ConsoleKey key)
        {
            switch (key)
            {
                case UpArrowPressed:
                    nextVerticalPlayerPosition--;
                    break;

                case DownArrowPressed:
                    nextVerticalPlayerPosition++;
                    break;

                case LeftArrowPressed:
                    nextHorizontalPlayerPosition--;
                    break;

                case RightArrowPressed:
                    nextHorizontalPlayerPosition++;
                    break;
            }
        }

        static bool IsBorders(int verticalPlayerDirection, int horizontalPlayerDirection, char[,] map)
        {
            bool canVerticalMove = verticalPlayerDirection < map.GetLength(0) && verticalPlayerDirection >= 0;
            bool canHorizontalMove = horizontalPlayerDirection < map.GetLength(1) && horizontalPlayerDirection >= 0;

            return canVerticalMove && canHorizontalMove;
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