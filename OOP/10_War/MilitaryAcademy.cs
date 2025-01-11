using System.Collections.Generic;

namespace _10_War
{
    public class MilitaryAcademy
    {
        private readonly Warrior[] _warriors;

        public MilitaryAcademy()
        {
            _warriors = new Warrior[]
            {
                new Solder(),
                new Sniper(),
                new Grenadier(),
                new MachineGunner()
            };
        }

        public List<Warrior> GetRandomWarriors(int count)
        {
            List<Warrior> warriors = new List<Warrior>(count);

            for (int i = 0; i < count; i++)
            {
                warriors.Add(_warriors[UserUtils.GenerateRandomNumber(_warriors.Length)].Clone());
            }

            return warriors;
        }
    }
}
