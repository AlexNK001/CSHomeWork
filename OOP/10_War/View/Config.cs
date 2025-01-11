using System;

namespace _10_War
{
    public struct Config
    {
        public readonly ConsoleColor Foreground;
        public readonly ConsoleColor Background;
        public readonly string Status;

        public Config(ConsoleColor foreground, ConsoleColor background, string status)
        {
            Foreground = foreground;
            Background = background;
            Status = status;
        }
    }
}
