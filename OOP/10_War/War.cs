
namespace _10_War
{
    public class War
    {
        private static void Main()
        {
            int numberSoldiersFirstPlatoon = 5;
            int numberSoldiersSecondPlatoon = 5;

            MilitaryAcademy academy = new MilitaryAcademy();
            Platoon firstPlatoon = new Platoon(academy.GetRandomWarriors(numberSoldiersFirstPlatoon));
            Platoon secondPlatoon = new Platoon(academy.GetRandomWarriors(numberSoldiersSecondPlatoon));
            DisplayBattlefield display = new DisplayBattlefield(firstPlatoon, secondPlatoon);
            Battlefield battlefield = new Battlefield(firstPlatoon, secondPlatoon);

            display.ShowBattleScreen();
            battlefield.StartBattle();
            display.ShowEndScreen();
        }
    }
}
