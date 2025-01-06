using System;

namespace _7_Passenger_train_configurator
{
    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int max) => s_random.Next(max);
        public static int GenerateRandomNumber(int min, int max) => s_random.Next(min, max);
    }
}
