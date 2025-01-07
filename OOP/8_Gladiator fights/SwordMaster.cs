﻿namespace _8_Gladiator_fights
{
    public class SwordMaster : Warrior
    {
        public SwordMaster() : base(1300f, 150f, 50f, 15, "Мастер меча", 6) { }

        public override void Attack(Warrior enemy)
        {
            float damage = Damage;

            if (WasWhereCanse)
            {
                int coifficent = 2;
                int bonusDamage = s_random.Next(Luck, Luck * coifficent);
                damage = AttackPower + bonusDamage;
            }

            enemy.TakeDamage(damage);
        }

        public override Warrior Clone() => new SwordMaster();

        public override void TakeDamage(float damage)
        {
            int number = s_random.Next((int)Health);

            if (number < damage)
            {
                float coifficent = 0.75f;
                damage *= coifficent;
            }

            if (WasWhereCanse)
            {
                DrinkHealingPotion();
            }

            base.TakeDamage(damage);
        }
    }
}
