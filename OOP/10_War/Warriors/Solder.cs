using System.Collections.Generic;
using System.Linq;

namespace _10_War
{
    public class Solder : Warrior
    {
        public Solder(
            string name = null,
            float health = 130,
            int minDamage = 30,
            int maxDamage = 45,
            float armor = 7)
            : base(name,
                  health,
                  minDamage,
                  maxDamage,
                  armor)
        { }

        public override Warrior Clone()
        {
            return new Solder();
        }

        protected override void ApplyAbility(List<Warrior> targets)
        {
            targets.First().TakeDamage(GetDamage());
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            if (enemies.Any(enemy => enemy.IsALive))
            {
                int targetIndex = 0;
                int minNumberTargets = 1;

                selectableTargets = enemies.Where(enemy => enemy.IsALive).ToList();

                if (selectableTargets.Count > minNumberTargets)
                {
                    targetIndex = UserUtils.GenerateRandomNumber(selectableTargets.Count);
                }

                Warrior target = selectableTargets[targetIndex];
                selectableTargets.Clear();
                selectableTargets.Add(target);
                return true;
            }
            else
            {
                selectableTargets = null;
                return false;
            }
        }
    }
}
