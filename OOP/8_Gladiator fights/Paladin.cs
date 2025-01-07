using System;

namespace _8_Gladiator_fights
{
    public class Paladin : Warrior
    {
        private readonly int _luckGain;
        private readonly int _firstHealthThresholdTreatment;
        private readonly int _secondHealthThresholdTreatment;
        private readonly int _thirdHealthThresholdTreatment;
        private readonly int _healthThresholdRaisingArmor;
        private readonly int _healthThresholdRaisingDamage;

        public Paladin() : base(950f, 120f, 50f, 18, "Паладин")
        {
            _luckGain = 2;
            _firstHealthThresholdTreatment = 70;
            _secondHealthThresholdTreatment = 50;
            _thirdHealthThresholdTreatment = 30;
            _healthThresholdRaisingArmor = 50;
            _healthThresholdRaisingDamage = 30;
        }

        public override Warrior Clone() => new Paladin();

        public override void Attack(Warrior enemy)
        {
            float coefficient = 1.5f;

            if (IsHealthLess(_healthThresholdRaisingDamage))
            {
                Console.WriteLine($"{Name}: Урон увеличен в {coefficient} раза.");
                enemy.TakeDamage(Damage * coefficient);
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
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

            TripleHealing();
        }

        private void TripleHealing()
        {
            if (IsHealthLess(_firstHealthThresholdTreatment))
            {
                DrinkHealingPotion();

                if (IsHealthLess(_secondHealthThresholdTreatment))
                {
                    DrinkHealingPotion();

                    if (IsHealthLess(_thirdHealthThresholdTreatment))
                    {
                        DrinkHealingPotion();
                    }
                }
            }
        }
    }
}
