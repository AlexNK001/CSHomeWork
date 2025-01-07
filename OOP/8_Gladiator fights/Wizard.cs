namespace _8_Gladiator_fights
{
    public class Wizard : Warrior
    {
        private int _mana;
        private readonly int _firstHealthThresholdTreatment;
        private readonly int _secondHealthThresholdTreatment;
        private float _refractDamage;

        public Wizard() : base(750f, 80f, 39f, 32, "Волшебник")
        {
            _mana = 250;
            _firstHealthThresholdTreatment = 60;
            _secondHealthThresholdTreatment = 40;
            _refractDamage = 0;
        }

        public override Warrior Clone() => new Wizard();

        public override void Attack(Warrior enemy)
        {
            int spellCount = 2;
            float bonusDamage = 0;

            if (TryCast())
            {
                bonusDamage += UseIncreasedDamage();
            }

            if (TryCast() && IsHealthLess(_firstHealthThresholdTreatment))
            {
                DrinkHealingPotion();
            }

            if (TryCast())
            {
                bonusDamage += UseRefractDamage();
            }

            if (TryCast() && IsHealthLess(_secondHealthThresholdTreatment))
            {
                DrinkHealingPotion();
                DrinkHealingPotion();
            }

            if (TryCast())
            {
                bonusDamage += UseDamageReturn();
            }

            enemy.TakeDamage(Damage + bonusDamage);

            bool TryCast()
            {
                if (WasWhereCanse && spellCount > 0)
                {
                    spellCount--;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override void TakeDamage(float damage)
        {
            float currentHealt = Health;

            base.TakeDamage(damage);

            float currentDamage = currentHealt - Health;

            if (currentDamage > _refractDamage)
            {
                _refractDamage = currentDamage;
            }
        }

        private float UseDamageReturn()
        {
            int manaCoast = 40;
            float maxDamageCoificent = 1.5f;
            float minDamageCoificent = 4f;

            if (_mana >= manaCoast)
            {
                _mana -= manaCoast;
                return (MaxHealth - Health) / maxDamageCoificent;
            }
            else
            {
                return (MaxHealth - Health) / minDamageCoificent;
            }
        }

        private float UseRefractDamage()
        {
            int manaCoast = 19;
            float coifficent = 4f;
            float minRefractDamage = 0;

            if (_mana >= manaCoast)
            {
                _refractDamage = minRefractDamage;
                _mana -= manaCoast;
                return _refractDamage;
            }
            else
            {
                _refractDamage = minRefractDamage;
                return _refractDamage / coifficent;
            }
        }

        private float UseIncreasedDamage()
        {
            int manaCoast = 10;
            int minBonus = 30;
            int maxBonus = 80;

            if (_mana >= manaCoast)
            {
                _mana -= manaCoast;
                return maxBonus;
            }
            else
            {
                return minBonus;
            }
        }
    }
}
