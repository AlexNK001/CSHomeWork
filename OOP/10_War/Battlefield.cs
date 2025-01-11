namespace _10_War
{
    public class Battlefield
    {
        private readonly Platoon _firstPlatoon;
        private readonly Platoon _secondPlatoon;

        public Battlefield(Platoon first, Platoon second)
        {
            _firstPlatoon = first;
            _secondPlatoon = second;
        }

        public void StartBattle()
        {
            while (_firstPlatoon.IsAlive && _secondPlatoon.IsAlive)
            {
                _firstPlatoon.Attack(_secondPlatoon.GetSolders());
                _secondPlatoon.Attack(_firstPlatoon.GetSolders());

                _firstPlatoon.RemoveDeadSolders();
                _secondPlatoon.RemoveDeadSolders();
            }
        }
    }
}
