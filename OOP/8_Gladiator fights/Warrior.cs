using System;

namespace _8_Gladiator_fights
{
    public abstract class Warrior
    {
        protected float Health;
        protected float MaxHealth;
        protected float AttackPower;
        protected float Armor;
        protected int Luck;
        protected int CountHealingPotion;
        protected float PotionPower;

        private readonly int _maxRandom;
        private readonly int _percent;
        public readonly string Name;

        public Warrior(
            float maxHealth,
            float attackPower,
            float armor,
            int luck,
            string name,
            int countHealingPotion = 4,
            float potionPower = 50)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            Armor = armor;
            Luck = luck;
            Name = name;
            CountHealingPotion = countHealingPotion;
            PotionPower = potionPower;

            _maxRandom = 101;
            _percent = 100;
        }

        public bool IsALive => Health > 0;
        public bool WasWhereCanse => Luck > UserUtils.GenerateRandomNumber(_maxRandom);
        public float Damage => UserUtils.GenerateRandomNumber((int)AttackPower, (int)AttackPower + Luck);

        public abstract Warrior Clone();

        public virtual void Attack(Warrior enemy)
        {
            enemy.TakeDamage(AttackPower);
        }

        public virtual void TakeDamage(float damage)
        {
            if (damage > 0)
            {
                float currentHealth = Health;
                int coefficient = 2;

                if (damage <= Armor)
                {
                    Health -= damage / coefficient;
                }
                else if (damage > Armor)
                {
                    Health -= Armor / coefficient + damage - Armor;
                }

                Console.WriteLine($"{Name} получил урон  {currentHealth - Health:F0}");
            }
            else
            {
                Console.WriteLine($"{Name} получает отрицательный урон!");
            }
        }

        public void ShowInfo(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.WriteLine($"{Name} {Health:F0}/{MaxHealth:F0}({GetHealthPercentage():F0}%)");
            Console.ResetColor();
        }

        public bool IsHealthLess(int healthPercent)
        {
            if (healthPercent > GetHealthPercentage())
            {
                Console.WriteLine($"{Name}: Уровень здоровья меньше {healthPercent}%");
            }

            return healthPercent > GetHealthPercentage();
        }

        protected void DrinkHealingPotions(int countHealingPotion = 1)
        {
            while (countHealingPotion > 0)
            {
                CountHealingPotion--;
                Health += PotionPower;
                Health = Math.Min(Health, MaxHealth);

                Console.WriteLine($"У {Name} произошло лечение на {PotionPower:F0} едениц. ХП банок/{CountHealingPotion}");

                countHealingPotion--;
            }
        }

        private float GetHealthPercentage()
        {
            return Health / MaxHealth * _percent; 
        }
    }
}
