using System;
using System.Collections.Generic;

namespace _10_War
{
    public abstract class Warrior 
    {
        private readonly float _maxHealth;
        private readonly float _armor;
        private readonly int _minDamage;
        private readonly int _maxDamage;
        private float _currentHealth;

        public Warrior(string name = null, float health = 130, int minDamage = 30, int maxDamage = 45, float armor = 7)
        {
            Name = name ?? ToString();
            _maxHealth = health;
            _currentHealth = _maxHealth;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _armor = armor;
        }

        public Action<Warrior> Attacked;
        public Action<Warrior> ReceivedDamage;

        public string Name { get; }
        public bool IsALive => _currentHealth > 0;

        public void Attack(IReadOnlyList<Warrior> enemies)
        {
            Attacked?.Invoke(this);

            if (TrySelectTargets(enemies, out List<Warrior> selectableTargets))
            {
                ApplyAbility(selectableTargets);
            }
        }

        public float GetShareHealth()
        {
            return _currentHealth / _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            int coefficient = 2;

            if (damage > 0 && damage <= _armor)
            {
                _currentHealth -= damage / coefficient;
            }
            else if (damage > 0 && damage > _armor)
            {
                _currentHealth -= _armor / coefficient + damage - _armor;
            }

            ReceivedDamage?.Invoke(this);
        }

        protected abstract bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets);

        protected abstract void ApplyAbility(List<Warrior> targets);

        public abstract Warrior Clone();

        protected int GetDamage()
        {
            return UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        }
    }
}
