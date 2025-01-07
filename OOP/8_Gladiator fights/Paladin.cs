using System;

namespace _8_Gladiator_fights
{
    public class Paladin : Warrior
    {
        private readonly int _luckGain;
        private readonly int _healthThresholdRaisingArmor;
        private readonly int _healthThresholdRaisingDamage;
        private readonly int[] _healthMultipleTreatments;

        public Paladin() : base(950f, 120f, 50f, 18, "Паладин")
        {
            _luckGain = 2;
            _healthThresholdRaisingArmor = 50;
            _healthThresholdRaisingDamage = 30;

            _healthMultipleTreatments = new int[] { 70, 50, 30 };
        }

        public override Warrior Clone()
        {
            return new Paladin();
        }

        public override void Attack(Warrior enemy)
        {
            float coefficient = 1.5f;
            float damage = Damage;

            if (IsHealthLess(_healthThresholdRaisingDamage))
            {
                Console.WriteLine($"{Name}: Урон увеличен в {coefficient} раза.");
                damage *= coefficient;
            }

            enemy.TakeDamage(damage);
        }

        public override void TakeDamage(float damage)
        {
            float bonusArmor = 0;

            if (IsHealthLess(_healthThresholdRaisingArmor))
            {
                bonusArmor = 10;
                Console.WriteLine($"{Name}: Колличество брони увеличено на {bonusArmor} едениц.");
            }

            if (WasWhereCanse)
            {
                float coefficient = 0.5f;
                Console.WriteLine($"{Name}: Текущий урон снижен в {coefficient} раза.");
                damage *= coefficient;
            }

            Luck += _luckGain;
            Armor += bonusArmor;

            base.TakeDamage(damage);

            Armor -= bonusArmor;

            MultipleHealing();
        }

        private void MultipleHealing()
        {
            for (int i = 0; i < _healthMultipleTreatments.Length; i++)
            {
                if (IsHealthLess(_healthMultipleTreatments[i]))
                {
                    DrinkHealingPotions();
                }
            }
        }
    }
}
