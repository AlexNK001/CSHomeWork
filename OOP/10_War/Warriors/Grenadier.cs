using System.Collections.Generic;

namespace _10_War
{
    public class Grenadier : Solder
    {
        public Grenadier(
            string name = null,
            float health = 150,
            int minDamage = 10,
            int maxDamage = 15,
            float armor = 8)
            : base(name,
                   health,
                   minDamage,
                   maxDamage,
                   armor)
        { }

        public override Solder Clone()
        {
            return new Grenadier();
        }

        protected override void ApplyAbility(List<Solder> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].TakeDamage(GetDamage());
            }
        }

        protected override bool TrySelectTargets(IReadOnlyList<Solder> enemies, out List<Solder> selectableTargets)
        {
            int maxTargetsCount = 4;
            selectableTargets = new List<Solder>(enemies);

            while (selectableTargets.Count > maxTargetsCount)
            {
                selectableTargets.RemoveAt(UserUtils.GenerateRandomNumber(selectableTargets.Count));
            }

            return true;
        }
    }
}
