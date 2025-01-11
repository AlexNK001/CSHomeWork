﻿using System.Collections.Generic;
using System.Linq;

namespace _10_War
{
    public class MachineGunner : Warrior
    {
        public MachineGunner(
            string name = null,
            float health = 130,
            int minDamage = 25,
            int maxDamage = 50,
            float armor = 7)
            : base(name,
                   health,
                   minDamage,
                   maxDamage,
                   armor)
        { }

        public override Warrior Clone()
        {
            return new MachineGunner();
        }

        protected override void ApplyAbility(List<Warrior> targets)
        {
            int singleTargetDamage = GetDamage() / targets.Count;

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(singleTargetDamage);
            }
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            if (enemies.Any(solder => solder.IsALive))
            {
                int targetsCount = 4;

                selectableTargets = enemies
                    .Where(solder => solder.IsALive)
                    .ToList();

                while (targetsCount < selectableTargets.Count)
                {
                    selectableTargets.RemoveAt(UserUtils.GenerateRandomNumber(selectableTargets.Count));
                }

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
