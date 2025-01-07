namespace _8_Gladiator_fights
{
    public class Barbarian : Warrior
    {
        private bool _isFirstAttack;
        private readonly int _healthThresholdRaisingDamage;

        public Barbarian() : base(1600f, 110f, 35f, 20, "Варвар")
        {
            _isFirstAttack = true;
            _healthThresholdRaisingDamage = 10;
        }

        public override Warrior Clone()
        {
            return new Barbarian();
        }

        public override void Attack(Warrior enemy)
        {
            float damage = Damage;
            int coeffcient = 2;

            if (_isFirstAttack)
            {
                damage *= coeffcient;
                _isFirstAttack = false;
            }

            if (IsHealthLess(_healthThresholdRaisingDamage) && WasWhereCanse)
            {
                damage *= coeffcient;
            }

            enemy.TakeDamage(damage);
        }

        public override void TakeDamage(float damage)
        {
            if (WasWhereCanse)
            {
                DrinkHealingPotions();
            }

            base.TakeDamage(damage);
        }
    }
}
