using System;
using System.Threading;

namespace Brave_new_world
{
    internal class Program
    {
        static void Main()
        {
            const ConsoleKey UpArrowPressed = ConsoleKey.UpArrow;
            const ConsoleKey DownArrowPressed = ConsoleKey.DownArrow;
            const ConsoleKey LeftArrowPressed = ConsoleKey.LeftArrow;
            const ConsoleKey RightArrowPressed = ConsoleKey.RightArrow;
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
            FindPlayer(map, out int verticalPosition, out int horizontalPosition, playerSymbol);

            while (isPlaying)
            {
                int nextVerticalPosition = verticalPosition;
                int nextHorizontalPosition = horizontalPosition;

                Console.SetCursorPosition(0, map.GetLength(0));
                Console.WriteLine($"Для выхода нажмите {CommandExit}");
                Console.SetCursorPosition(0, 0);

                switch (Console.ReadKey(true).Key)
                {
                    case UpArrowPressed:
                        nextVerticalPosition--;
                        break;

                    case DownArrowPressed:
                        nextVerticalPosition++;
                        break;

                    case LeftArrowPressed:
                        nextHorizontalPosition--;
                        break;

                    case RightArrowPressed:
                        nextHorizontalPosition++;
                        break;

                    case CommandExit:
                        isPlaying = false;
                        break;
                }

                if (CheckBorders(nextVerticalPosition, nextHorizontalPosition, map))
                {
                    if (map[nextVerticalPosition, nextHorizontalPosition] != wallSymbol)
                    {
                        DrawOldSymbol(verticalPosition, horizontalPosition, map, wallSymbol, voidSymbol);
                        Move(ref verticalPosition, ref horizontalPosition, nextVerticalPosition, nextHorizontalPosition);
                        DrawSymbol(verticalPosition, horizontalPosition, playerSymbol);
                    }
                }

                Thread.Sleep(FrameDuration);
            }
        }

        static bool CheckBorders(int verticalDirection, int horizontalDirection, char[,] map)
        {
            bool canVerticalMove = verticalDirection < map.GetLength(0) && verticalDirection >= 0;
            bool canHorizontalMove = horizontalDirection < map.GetLength(1) && horizontalDirection >= 0;

            return canVerticalMove && canHorizontalMove;
        }

        static void Move(ref int nextVerticalPosition, ref int playerY, int directionX, int directionY)
        {
            nextVerticalPosition = directionX;
            playerY = directionY;
        }

        static void DrawSymbol(int playerX, int playerY, char symbol)
        {
            Console.SetCursorPosition(playerY, playerX);
            Console.Write(symbol);
        }

        static void DrawOldSymbol(int playerX, int playerY, char[,] map, char symbolWall, char symbolVoid)
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
