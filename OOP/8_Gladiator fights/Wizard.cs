using System;

namespace _8_Gladiator_fights
{
    public class Wizard : Warrior
    {
        private readonly int _firstHealthThresholdTreatment;
        private readonly int _secondHealthThresholdTreatment;
        private readonly int _maxSpellCount;
        private int _currentSpellCount;
        private int _mana;
        private float _refractDamage;

        public Wizard() : base(750f, 80f, 39f, 32, "Волшебник")
        {
            _mana = 250;
            _firstHealthThresholdTreatment = 60;
            _secondHealthThresholdTreatment = 40;
            _refractDamage = 0;
            _maxSpellCount = 2;
            _currentSpellCount = _maxSpellCount;
        }

        public override Warrior Clone()
        {
            return new Wizard();
        }

        public override void Attack(Warrior enemy)
        {
            _currentSpellCount = _maxSpellCount;
            float finalDamage = Damage;

            if (TryCast())
            {
                finalDamage += UseIncreasedDamage();
            }

            if (TryCast() && IsHealthLess(_firstHealthThresholdTreatment))
            {
                DrinkHealingPotions();
            }

            if (TryCast())
            {
                finalDamage += UseRefractDamage();
            }

            if (TryCast() && IsHealthLess(_secondHealthThresholdTreatment))
            {
                DrinkHealingPotions(2);
            }

            if (TryCast())
            {
                finalDamage += UseDamageReturn();
            }

            enemy.TakeDamage(finalDamage);
        }

        public override void TakeDamage(float damage)
        {
            float currentHealt = Health;

            base.TakeDamage(damage);

            float currentDamage = currentHealt - Health;
            _refractDamage = Math.Max(currentDamage, _refractDamage);
        }

        private float UseDamageReturn()
        {
            int manaCoast = 40;
            float maxDamageCoefficient = 1.5f;
            float minDamageCoefficient = 4f;

            float damageCoefficient = TrySpendMana(manaCoast) ? maxDamageCoefficient : minDamageCoefficient;

            return (MaxHealth - Health) / damageCoefficient;
        }

        private float UseRefractDamage()
        {
            int manaCoast = 19;
            float coefficient = 4f;
            float minRefractDamage = 0;

            float damage = TrySpendMana(manaCoast) ? _refractDamage : _refractDamage / coefficient;
            _refractDamage = minRefractDamage;

            return damage;
        }

        private float UseIncreasedDamage()
        {
            int manaCoast = 10;
            int minBonus = 30;
            int maxBonus = 80;

            return TrySpendMana(manaCoast) ? maxBonus : minBonus;
        }

        private bool TrySpendMana(int manaCoast)
        {
            bool haveMana = _mana >= manaCoast;

            if (haveMana)
                _mana -= manaCoast;

            return haveMana;
        }

        private bool TryCast()
        {
            bool haveCast = WasWhereCanse && _currentSpellCount > 0;

            if (haveCast)
                _currentSpellCount--;

            return haveCast;
        }
    }
}
