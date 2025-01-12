using System;

namespace _10_War
{
    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static int GenerateRandomNumber(int max)
        {
            return s_random.Next(max);
        }
    }
}
