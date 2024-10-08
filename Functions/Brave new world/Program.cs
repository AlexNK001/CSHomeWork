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
            const ConsoleKey CommandDrawingMode = ConsoleKey.Q;
            const ConsoleKey CommandDraw = ConsoleKey.W;
            const ConsoleKey CommandExit = ConsoleKey.Escape;

            int playerPositionX;
            int playerPositionY;
            bool canPaint = false;
            bool isPlaying = true;
            bool isDrawing = false;
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
            FindPlayer(map, out playerPositionX, out playerPositionY, playerSymbol);

            int lastPlayerX = playerPositionX;
            int lastPlayerY = playerPositionY;

            while (isPlaying)
            {
                int directionX = 0;
                int directionY = 0;

                Console.SetCursorPosition(0, map.GetLength(0));
                Console.WriteLine($"Режим рисования {canPaint}");
                Console.WriteLine($"Для включения режима рисования нажмите {CommandDrawingMode}");
                Console.WriteLine($"Для рисования в режиме рисования нажмите {CommandDraw}");
                Console.WriteLine($"Для выхода нажмите {CommandExit}");
                Console.SetCursorPosition(0, 0);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case UpArrowPressed:
                            ChoiceDirection(ref directionX, ref directionY, -1, 0);
                            break;

                        case DownArrowPressed:
                            ChoiceDirection(ref directionX, ref directionY, 1, 0);
                            break;

                        case LeftArrowPressed:
                            ChoiceDirection(ref directionX, ref directionY, 0, -1);
                            break;

                        case RightArrowPressed:
                            ChoiceDirection(ref directionX, ref directionY, 0, 1);
                            break;

                        case CommandDrawingMode:
                            EnableDrawingMode(ref canPaint, map, ref lastPlayerX, ref lastPlayerY, playerPositionX, playerPositionY, wallSymbol, voidSymbol);
                            break;

                        case CommandDraw:
                            isDrawing = true;
                            break;

                        case CommandExit:
                            isPlaying = false;
                            break;
                    }
                }

                if (CheckBorder(playerPositionX, directionX, map, 0) && CheckBorder(playerPositionY, directionY, map, 1))
                {
                    if (canPaint == true && isDrawing == true)
                    {
                        DrawSymbol(playerPositionX, playerPositionY, wallSymbol);
                        ReversalSymbol(map, playerPositionX, playerPositionY, wallSymbol, voidSymbol);
                        Move(ref playerPositionX, ref playerPositionY, directionX, directionY);
                        DeleteObsoletePosition(map, ref lastPlayerX, ref lastPlayerY, playerPositionX, playerPositionY, wallSymbol, voidSymbol);
                        DrawMap(map);

                    }
                    else if (canPaint == true)
                    {
                        DrawSymbol(playerPositionX, playerPositionY, map[playerPositionX, playerPositionY]);
                        Move(ref playerPositionX, ref playerPositionY, directionX, directionY);
                        DrawSymbol(playerPositionX, playerPositionY, playerSymbol);
                    }
                    else if (map[playerPositionX + directionX, playerPositionY + directionY] != wallSymbol)
                    {
                        DrawOldSymbol(playerPositionX, playerPositionY, map, wallSymbol, voidSymbol);
                        Move(ref playerPositionX, ref playerPositionY, directionX, directionY);
                        DrawSymbol(playerPositionX, playerPositionY, playerSymbol);
                    }

                    isDrawing = false;
                }

                Thread.Sleep(200);
            }
        }

        static char[,] ReversalSymbol(char[,] map, int playerX, int playerY, char first, char second)
        {
            if (map[playerX, playerY] == first)
                map[playerX, playerY] = second;
            else if (map[playerX, playerY] == second)
                map[playerX, playerY] = first;

            return map;
        }

        static bool CheckBorder(int axis, int direction, char[,] map, int dimensionNumber)
        {
            bool canMove = axis + direction < map.GetLength(dimensionNumber) && axis + direction >= 0;

            return canMove;
        }

        static void ChoiceDirection(ref int directionX, ref int directionY, int axisX, int axisY)
        {
            directionX += axisX;
            directionY += axisY;
        }

        static void Move(ref int playerX, ref int playerY, int directionX, int directionY)
        {
            playerX += directionX;
            playerY += directionY;
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

        static void EnableDrawingMode(ref bool isPaint, char[,] map, ref int lastX, ref int lastY, int playerX, int playerY, char symbolWall, char symbolVoid)
        {
            if (isPaint == false)
                isPaint = true;
            else
                isPaint = false;

            DeleteObsoletePosition(map, ref lastX, ref lastY, playerX, playerY, symbolWall, symbolVoid);
            DrawMap(map);
        }

        static void DeleteObsoletePosition(char[,] map, ref int lastX, ref int lastY, int playerX, int playerY, char symbolWall, char symbolVoid)
        {
            if (map[lastX, lastY] != symbolWall)
                map[lastX, lastY] = symbolVoid;

            lastX = playerX; lastY = playerY;
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
