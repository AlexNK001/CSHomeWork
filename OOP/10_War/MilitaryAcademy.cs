using System.Collections.Generic;

namespace _10_War
{
    public class MilitaryAcademy
    {
        private readonly Solder[] _solders;

        public MilitaryAcademy()
        {
            _solders = new Solder[]
            {
                new Solder(),
                new Sniper(),
                new Grenadier(),
                new MachineGunner()
            };
        }

        public List<Solder> GetRandomWarriors(int count)
        {
            List<Solder> solders = new List<Solder>(count);

            for (int i = 0; i < count; i++)
            {
                solders.Add(_solders[UserUtils.GenerateRandomNumber(_solders.Length)].Clone());
            }

            return solders;
        }
    }
}
