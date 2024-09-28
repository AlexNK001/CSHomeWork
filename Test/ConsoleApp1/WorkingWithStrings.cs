using System;

public class WorkingWithStrings
{
    static void Main()
    {
        Console.Write("Введите Ваше имя");
        string name = Console.ReadLine();
        Console.Clear();

        Console.Write("Введите Ваш возраст");
        string age = Console.ReadLine();
        Console.Clear();

        Console.Write("Введите Ваше место работы");
        string placeWork = Console.ReadLine();
        Console.Clear();

        Console.WriteLine($"Ваше имя {name}, возраст {age} и работаете в {placeWork}.");

        Console.ReadKey();
    }
}