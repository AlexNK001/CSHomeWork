using System;

namespace _2_Working_with_properties
{
    public class Program
    {
        static void Main()
        {
            Player player = new Player(4, 5, '@');
            Renderer renderer = new Renderer();

            renderer.Draw(player);

            Console.ReadKey();
        }
    }

    public class Player
    {
        public Player(int positionX, int positionY, char symbolPersonage)
        {
            Symbol = symbolPersonage;
            PositionX = positionX;
            PositionY = positionY;
        }

        public char Symbol { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
    }

    public class Renderer
    {
        public void Draw(Player player)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Symbol);
            Console.ReadKey(true);
        }
    }
}
