using System;
using System.Collections.Generic;
using System.Linq;

namespace _10_War
{
    public class Solder
    {
        private readonly float _maxHealth;
        private readonly float _armor;
        private readonly int _minDamage;
        private readonly int _maxDamage;
        private float _currentHealth;

        public Solder(string name = null, float health = 130, int minDamage = 30, int maxDamage = 45, float armor = 7)
        {
            Name = name ?? ToString();
            _maxHealth = health;
            _currentHealth = _maxHealth;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _armor = armor;
        }

        public event Action<Solder> Attacked;
        public event Action<Solder> ReceivedDamage;

        public string Name { get; }
        public bool IsALive => _currentHealth > 0;
        public float PercentageHealth() => _currentHealth / _maxHealth;

        public virtual Solder Clone()
        {
            return new Solder();
        }

        public void Attack(IReadOnlyList<Solder> enemies)
        {
            Attacked?.Invoke(this);

            if (TrySelectTargets(enemies, out List<Solder> selectableTargets))
            {
                ApplyAbility(selectableTargets);
            }
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

        protected virtual void ApplyAbility(List<Solder> targets)
        {
            targets.First().TakeDamage(GetDamage());
        }

        protected virtual bool TrySelectTargets(IReadOnlyList<Solder> enemies, out List<Solder> selectableTargets)
        {
            if (enemies.Any(enemy => enemy.IsALive))
            {
                int targetIndex = 0;
                int minNumberTargets = 1;

                selectableTargets = enemies.Where(enemy => enemy.IsALive).ToList();

                if (selectableTargets.Count > minNumberTargets)
                {
                    targetIndex = UserUtils.GenerateRandomNumber(selectableTargets.Count);
                }

                Solder target = selectableTargets[targetIndex];
                selectableTargets.Clear();
                selectableTargets.Add(target);
                return true;
            }
            else
            {
                selectableTargets = null;
                return false;
            }
        }

        protected virtual int GetDamage()
        {
            return UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        }
    }
}
