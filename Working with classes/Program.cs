using System;

namespace Working_with_classes
{
    public static class Program
    {
        private static void Main()
        {
            Player player1 = new Player("Борис", 100, 30, 12, 30, 14);
            Player player2 = new Player("Джони", 120, 28, 13, 28, 18);

            player1.ShowStats();
            player2.ShowStats();

            Console.ReadKey();
        }
    }

    public class Player
    {
        private string _name;
        private int _health;
        private int _damage;
        private int _armor;
        private int _speed;
        private int _atackSpeed;

        public Player(string name, int health, int damage, int armor, int speed, int atackSpeed)
        {
            _name = name;
            _health = health;
            _damage = damage;
            _armor = armor;
            _speed = speed;
            _atackSpeed = atackSpeed;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Имя - {_name}");
            Console.WriteLine($"Здоровье - {_health}");
            Console.WriteLine($"Урон - {_damage}");
            Console.WriteLine($"Защита - {_armor}");
            Console.WriteLine($"Скорость - {_speed}");
            Console.WriteLine($"Скорость атаки - {_atackSpeed}\n");
        }
    }
}
