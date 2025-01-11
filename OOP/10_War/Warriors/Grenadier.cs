using System.Collections.Generic;

namespace _10_War
{
    public class Grenadier : Warrior
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

        public override Warrior Clone()
        {
            return new Grenadier();
        }

        protected override void ApplyAbility(List<Warrior> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].TakeDamage(GetDamage());
            }
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            int maxTargetsCount = 4;
            selectableTargets = new List<Warrior>(enemies);

            while (selectableTargets.Count > maxTargetsCount)
            {
                selectableTargets.RemoveAt(UserUtils.GenerateRandomNumber(selectableTargets.Count));
            }

            return true;
        }
    }
}
