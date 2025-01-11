using System.Collections.Generic;
using System.Linq;

namespace _10_War
{
    public class Sniper : Warrior
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

        public override Warrior Clone()
        {
            return new Sniper();
        }

        protected override void ApplyAbility(List<Warrior> targets)
        {
            int damageMultiplier = 2;
            int damage = GetDamage() * damageMultiplier;
            targets.First().TakeDamage(damage);
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            if (enemies.Any(solder => solder.IsALive))
            {
                Warrior target = enemies
                    .Where(solder => solder.IsALive)
                    .OrderBy(healt => healt.GetShareHealth())
                    .First();

                selectableTargets = new List<Warrior> { target };
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
