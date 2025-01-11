using System;
using System.Collections.Generic;
using System.Linq;

namespace _10_War
{
    public class War
    {
        private static void Main()
        {
            //List<Test> tests = new List<Test> { new Test(1, 7, 3), new Test(4, 5, 6), new Test(7, 1, 9) };

            ////tests = tests.OrderBy(t => t.Second).ToList();

            //Test test = tests.OrderBy(t => t.Second).First();
            //Console.WriteLine($"{test.First} {test.Second} {test.Third}");


            //return;
            Solder solder3 = new Solder("pl.1 - S.3");
            Solder solder4 = new Solder("pl.2 - S.4");

            List<Warrior> firstSolders = new List<Warrior>
            {
                new Sniper("Sniper"),
                new Grenadier("Grenadier"),
                solder3,
                new Solder("pl.1 - S.4"),
                new Solder("pl.1 - S.5"),
            };

            List<Warrior> secondSolders = new List<Warrior>
            {
                new MachineGunner("MachineGunner"),
                new Solder("pl.2 - S.1"),
                new Solder("pl.2 - S.2"),
                new Solder("pl.2 - S.3"),
                solder4,
            };


            Platoon firstPlatoon = new Platoon(firstSolders);
            Platoon secondPlatoon = new Platoon(secondSolders);
            TextStorage textStorage = new TextStorage(firstPlatoon, secondPlatoon);

            Battlefield battlefield = new Battlefield(firstPlatoon, secondPlatoon);
            textStorage.ShowBattleScreen();

            battlefield.StartBattle();
        }
    }

    public class Battlefield
    {
        private readonly Platoon _firstPlatoon;
        private readonly Platoon _secondPlatoon;

        public Battlefield(Platoon first, Platoon second)
        {
            _firstPlatoon = first;
            _secondPlatoon = second;
        }

        public void StartBattle()
        {
            while (_firstPlatoon.IsAlive && _secondPlatoon.IsAlive)
            {
                _firstPlatoon.Attack(_secondPlatoon.GetSolders());
                _secondPlatoon.Attack(_firstPlatoon.GetSolders());

                _firstPlatoon.RemoveDeadSolders();
                _secondPlatoon.RemoveDeadSolders();
            }
        }
    }

    public class Platoon
    {
        private readonly List<Warrior> _solders;

        public Platoon(List<Warrior> solders)
        {
            _solders = solders;
        }

        public bool IsAlive => _solders.Count > 0;

        public void Attack(IReadOnlyList<Warrior> solders)
        {
            for (int i = 0; i < _solders.Count; i++)
            {
                _solders[i].Attack(solders);
            }
        }

        public IReadOnlyList<Warrior> GetSolders()
        {
            return _solders;
        }

        public void RemoveDeadSolders()
        {
            int solderCount = _solders.Count;

            for (int i = solderCount - 1; i >= 0; i--)
            {
                Warrior currentSolder = _solders[i];

                if (currentSolder.IsALive == false)
                    _solders.Remove(currentSolder);
            }
        }
    }

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

        protected int GetDamage()
        {
            return UserUtils.GenerateRandomNumber(_minDamage, _maxDamage);
        }
    }

    public class Solder : Warrior
    {
        public Solder(
            string name = null,
            float health = 130,
            int minDamage = 30,
            int maxDamage = 45,
            float armor = 7)
            : base(name,
                  health,
                  minDamage,
                  maxDamage,
                  armor)
        { }

        protected override void ApplyAbility(List<Warrior> targets)
        {
            targets.First().TakeDamage(GetDamage());
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
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

                Warrior target = selectableTargets[targetIndex];
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
    }


    public class Grenadier : Warrior
    {
        public Grenadier(
            string name = null,
            float health = 150,
            int minDamage = 10,
            int maxDamage = 15,
            float armor = 8)
            : base(name,
                   health,
                   minDamage,
                   maxDamage,
                   armor)
        { }

        protected override void ApplyAbility(List<Warrior> enemies)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].TakeDamage(GetDamage());
            }
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            int maxTargetsCount = 4;
            selectableTargets = new List<Warrior>(enemies);

            while (selectableTargets.Count > maxTargetsCount)
            {
                selectableTargets.RemoveAt(UserUtils.GenerateRandomNumber(selectableTargets.Count));
            }

            return true;
        }
    }

    public class MachineGunner : Warrior
    {
        public MachineGunner(
            string name = null,
            float health = 130,
            int minDamage = 25,
            int maxDamage = 50,
            float armor = 7)
            : base(name,
                   health,
                   minDamage,
                   maxDamage,
                   armor)
        { }

        protected override void ApplyAbility(List<Warrior> targets)
        {
            int singleTargetDamage = GetDamage() / targets.Count;

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(singleTargetDamage);
            }
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            if (enemies.Any(solder => solder.IsALive))
            {
                int targetsCount = 4;

                selectableTargets = enemies
                    .Where(solder => solder.IsALive)
                    .ToList();

                while (targetsCount < selectableTargets.Count)
                {
                    selectableTargets.RemoveAt(UserUtils.GenerateRandomNumber(selectableTargets.Count));
                }

                return true;
            }
            else
            {
                selectableTargets = null;
                return false;
            }
        }
    }
    public class Sniper : Warrior
    {
        public Sniper(
            string name = null,
            float health = 120,
            int minDamage = 30,
            int maxDamage = 45,
            float armor = 5)
            : base(name,
                   health,
                   minDamage,
                   maxDamage,
                   armor)
        { }

        protected override void ApplyAbility(List<Warrior> targets)
        {
            int damageMultiplier = 2;
            int damage = GetDamage() * damageMultiplier;
            targets.First().TakeDamage(damage);
        }

        protected override bool TrySelectTargets(IReadOnlyList<Warrior> enemies, out List<Warrior> selectableTargets)
        {
            if (enemies.Any(solder => solder.IsALive))
            {
                Warrior target = enemies
                    .Where(solder => solder.IsALive)
                    .OrderBy(healt => healt.GetShareHealth())
                    .First();

                selectableTargets = new List<Warrior> { target };
                return true;
            }
            else
            {
                selectableTargets = null;
                return false;
            }
        }
    }
}
