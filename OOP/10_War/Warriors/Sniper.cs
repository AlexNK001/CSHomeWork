using System.Collections.Generic;
using System.Linq;

namespace _10_War
{
    public class Sniper : Solder
    {
        public Sniper(
            string name = null,
            float health = 120,
            int minDamage = 30,
            int maxDamage = 45,
            float armor = 5)
            : base(name,
                   health,
                   minDamage,
                   maxDamage,
                   armor)
        { }

        public override Solder Clone()
        {
            return new Sniper();
        }

        protected override void ApplyAbility(List<Solder> targets)
        {
            int damageMultiplier = 2;
            int damage = GetDamage() * damageMultiplier;
            targets.First().TakeDamage(damage);
        }

        protected override bool TrySelectTargets(IReadOnlyList<Solder> enemies, out List<Solder> selectableTargets)
        {
            if (enemies.Any(solder => solder.IsALive))
            {
                Solder target = enemies
                    .Where(solder => solder.IsALive)
                    .OrderBy(healt => healt.PercentageHealth())
                    .First();

                selectableTargets = new List<Solder> { target };
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
