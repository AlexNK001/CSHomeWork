using System;

namespace _8_Gladiator_fights
{
    public class Archer : Warrior
    {
        private int _countDistanseAttak;
        private readonly float _bonusDistantDamage;
        private readonly int _healthThresholdTreatment;

        public Archer() : base(700f, 140f, 50f, 15, "Лучник", potionPower: 55)
        {
            _countDistanseAttak = 3;
            _bonusDistantDamage = 50;
            _healthThresholdTreatment = 60;
        }

        public override Warrior Clone() => new Archer();

        public override void Attack(Warrior enemy)
        {
            int minCountDistanceAttack = 0;

            if (_countDistanseAttak > minCountDistanceAttack)
            {
                _countDistanseAttak--;
                enemy.TakeDamage(Damage + _bonusDistantDamage);
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
        }

        public override void TakeDamage(float damage)
        {
            int minCountDistanceAttack = 0;

            if (IsHealthLess(_healthThresholdTreatment) && WasWhereCanse)
                DrinkHealingPotion();

            if (_countDistanseAttak > minCountDistanceAttack)
            {
                Console.WriteLine($"{Name} отбежал от противника и не получил урон.");
            }
            else
            {
                base.TakeDamage(damage);

                if (WasWhereCanse)
                {
                    Console.WriteLine($"{Name} отбежал от противника");
                    _countDistanseAttak++;
                }
            }
        }
    }
}
