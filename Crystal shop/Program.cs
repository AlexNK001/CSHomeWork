using System;

namespace Crystal_shop
{
    class Program
    {
        static void Main()
        {
            int priceCrystal = 3;

            Console.Write("Введите количество Вашего золота");
            int amountGold = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.Write("Cколько кристаллов вы хотите купить?");
            int amountCrystals = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            amountGold -= amountCrystals * priceCrystal;
            Console.Write($"Золота - {amountGold}, кристалов - {amountCrystals}.");
            Console.ReadKey();
        }
    }
}
