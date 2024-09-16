using System;

namespace Pictures
{
    class Program
    {
        static void Main()
        {
            int numberPictures = 52;
            int rowCapacity = 3;
            int filledRows = numberPictures / rowCapacity;
            int extraPictures = numberPictures % rowCapacity;

            Console.WriteLine($"Количество заполненных рядов - {filledRows}, картинок сверх меры - {extraPictures}.");
            Console.ReadKey();
        }
    }
}
