using System;
using System.Collections.Generic;

namespace _8_Gladiator_fights
{
    public class Lesson_8
    {
        static void Main()
        {
            Arena arena = new Arena();
            arena.Work();
        }
    }

    public class Arena
    {
        private readonly List<Warrior> _warriors;
        private Warrior _firstWarrior;
        private Warrior _secondWarrior;
        private bool _isReady = false;
        private readonly ConsoleColor _firstColor = ConsoleColor.Red;
        private readonly ConsoleColor _secondColor = ConsoleColor.Blue;

        public Arena()
        {
            _warriors = GetWarriors();
        }

        public void Work()
        {
            const string CommmandSelectPlayers = "1";
            const string CommandStartBattle = "2";
            const string CommmandExit = "3";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{CommmandSelectPlayers} - выбрать игроков");
                Console.WriteLine($"{CommandStartBattle} - начать битву");
                Console.WriteLine($"{CommmandExit} - выйти из игры");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommmandSelectPlayers:
                        _isReady = TrySelectWarriors();
                        break;

                    case CommandStartBattle:
                        StartBattle();
                        break;

                    case CommmandExit:
                        isWork = false;
                        break;
                }

                Console.Clear();
            }
        }

        private void StartBattle()
        {
            bool isWork = true;

            if (_isReady)
            {
                while (isWork)
                {
                    ExchangeBlows();
                    isWork = HaveLiveWarriors();
                    Console.ReadKey();
                }

                _isReady = false;
            }
            else
            {
                Console.WriteLine("Выберите игроков");
            }
        }

        private bool TrySelectWarriors()
        {
            ShowNames();

            Console.WriteLine("Выберите первого игрока.");

            if (TrySelectWarrior(out Warrior firstWarrior))
            {
                Console.WriteLine("Выберите второго игрока.");

                if (TrySelectWarrior(out Warrior secondWarrior))
                {
                    _firstWarrior = firstWarrior.Clone();
                    _secondWarrior = secondWarrior.Clone();

                    _isReady = true;

                    return true;
                }
            }

            return false;
        }

        private bool HaveLiveWarriors()
        {
            if (_firstWarrior.IsALive == false && _secondWarrior.IsALive == false)
            {
                Console.WriteLine("Игроки убили друг друга. Ничья.");
            }
            else if (_firstWarrior.IsALive == false)
            {
                Console.WriteLine($"Первый игрок: {_firstWarrior.Name} пал. Второй игрок: {_secondWarrior.Name} выйграл!");
            }
            else if (_secondWarrior.IsALive == false)
            {
                Console.WriteLine($"Второй игрок: {_secondWarrior.Name} пал. Первый игрок: {_firstWarrior.Name} выйграл!");
            }

            return _firstWarrior.IsALive && _secondWarrior.IsALive;
        }

        private void ExchangeBlows()
        {
            _firstWarrior.Attack(_secondWarrior);
            _secondWarrior.ShowInfo(_secondColor);

            Console.WriteLine();

            _secondWarrior.Attack(_firstWarrior);
            _firstWarrior.ShowInfo(_firstColor);
        }

        private bool TrySelectWarrior(out Warrior warrior)
        {
            warrior = null;

            bool haveInput = int.TryParse(Console.ReadLine(), out int result);
            bool canResult = result > 0 && result <= _warriors.Count;

            if (canResult && haveInput)
            {
                warrior = _warriors[result - 1];
            }
            else if (haveInput == false)
            {
                Console.WriteLine("Некорректный ввод.");
            }
            else if (canResult == false)
            {
                Console.WriteLine("Участника с таким номером не существует.");
            }

            return canResult && haveInput;
        }

        private List<Warrior> GetWarriors()
        {
            return new List<Warrior>
            {
                new Paladin(),
                new Archer(),
                new Wizard(),
                new Barbarian(),
                new SwordMaster(),
            };
        }

        private void ShowNames()
        {
            int number = 1;

            foreach (Warrior warrior in _warriors)
            {
                Console.WriteLine($"{number})- {warrior.Name}");
                number++;
            }
        }
    }

    public abstract class Warrior
    {
        protected static Random s_random = new Random();

        protected float Health;
        protected float MaxHealth;
        protected float AttackPower;
        protected float Armor;
        protected int Luck;
        protected int CountHealingPotion;
        protected float PotionPower;

        protected int TenPercentHealth;
        protected int ThirtyPercentHealth;
        protected int FourtyPercentHealth;
        protected int FiftyPercentHealth;
        protected int SixtyPercentHealth;
        protected int SeventyPercentHealth;
        private readonly int _maxRandom;
        private readonly int _percent;
        public readonly string Name;

        public bool IsALive => Health > 0;
        protected bool WasWhereCanse => Luck > s_random.Next(_maxRandom);
        protected float Damage => s_random.Next((int)AttackPower, (int)AttackPower + Luck);
        private float HealthPercentage => Health / MaxHealth * _percent;

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

            TenPercentHealth = 10;
            ThirtyPercentHealth = 30;
            FourtyPercentHealth = 40;
            FiftyPercentHealth = 50;
            SixtyPercentHealth = 60;
            SeventyPercentHealth = 70;
            _maxRandom = 101;
            _percent = 100;
        }

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
            Console.WriteLine($"{Name} {Health:F0}/{MaxHealth:F0}({HealthPercentage:F0}%)");
            Console.ResetColor();
        }

        public bool IsHealthLess(int healthPercent)
        {
            if (healthPercent > HealthPercentage)
            {
                Console.WriteLine($"{Name}: Уровень здоровья меньше {healthPercent}%");
            }

            return healthPercent > HealthPercentage;
        }

        protected void DrinkHealingPotion()
        {
            if (CountHealingPotion > 0)
            {
                CountHealingPotion--;
                Health += PotionPower;

                Console.WriteLine($"У {Name} произошло лечение на {PotionPower:F0} едениц. ХП банок/{CountHealingPotion}");

                if (Health >= MaxHealth)
                {
                    Health = MaxHealth;
                }
            }
        }
    }

    public class Paladin : Warrior
    {
        private readonly int _luckGain;

        public Paladin() : base(950f, 120f, 50f, 18, "Паладин")
        {
            _luckGain = 2;
        }

        public override Warrior Clone() => new Paladin();

        public override void Attack(Warrior enemy)
        {
            float coefficient = 1.5f;

            if (IsHealthLess(ThirtyPercentHealth))
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

            if (IsHealthLess(FiftyPercentHealth))
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
            if (IsHealthLess(SeventyPercentHealth))
            {
                DrinkHealingPotion();

                if (IsHealthLess(FiftyPercentHealth))
                {
                    DrinkHealingPotion();

                    if (IsHealthLess(ThirtyPercentHealth))
                    {
                        DrinkHealingPotion();
                    }
                }
            }
        }
    }

    public class Archer : Warrior
    {
        private int _countDistanseAttak;
        private readonly float _bonusDistantDamage;

        public Archer() : base(700f, 140f, 50f, 15, "Лучник", potionPower: 55)
        {
            _countDistanseAttak = 3;
            _bonusDistantDamage = 50;
        }

        public override Warrior Clone() => new Archer();

        public override void Attack(Warrior enemy)
        {
            if (_countDistanseAttak > 0)
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
            if (IsHealthLess(SixtyPercentHealth) && WasWhereCanse)
                DrinkHealingPotion();

            if (_countDistanseAttak > 0)
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

    public class Wizard : Warrior
    {
        private int _mana;

        public Wizard() : base(750f, 80f, 39f, 32, "Волшебник")
        {
            _mana = 250;
        }

        public override Warrior Clone() => new Wizard();

        private float RefractDamage { get; set; } = 0;

        public override void Attack(Warrior enemy)
        {
            int spellCount = 2;
            float bonusDamage = 0;

            if (TryCast())
            {
                bonusDamage += UseIncreasedDamage();
            }

            if (TryCast() && IsHealthLess(SixtyPercentHealth))
            {
                DrinkHealingPotion();
            }

            if (TryCast())
            {
                bonusDamage += UseRefractDamage();
            }

            if (TryCast() && IsHealthLess(FourtyPercentHealth))
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

            if (currentDamage > RefractDamage)
            {
                RefractDamage = currentDamage;
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

            if (_mana >= manaCoast)
            {
                RefractDamage = 0;
                _mana -= manaCoast;
                return RefractDamage;
            }
            else
            {
                RefractDamage = 0;
                return RefractDamage / coifficent;
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

    public class SwordMaster : Warrior
    {
        public SwordMaster() : base(1300f, 150f, 50f, 15, "Мастер меча", 6) { }

        public override void Attack(Warrior enemy)
        {
            float damage = Damage;

            if (WasWhereCanse)
            {
                int coifficent = 2;
                int bonusDamage = s_random.Next(Luck, Luck * coifficent);
                damage = AttackPower + bonusDamage;
            }

            enemy.TakeDamage(damage);
        }

        public override Warrior Clone() => new SwordMaster();

        public override void TakeDamage(float damage)
        {
            int number = s_random.Next(0, (int)Health);

            if (number < damage)
            {
                float coifficent = 0.75f;
                damage *= coifficent;
            }

            if (WasWhereCanse)
            {
                DrinkHealingPotion();
            }

            base.TakeDamage(damage);
        }
    }

    public class Barbarian : Warrior
    {
        private bool _isFirstAttack;

        public Barbarian() : base(1600f, 110f, 35f, 20, "Варвар")
        {
            _isFirstAttack = true;
        }

        public override Warrior Clone() => new Barbarian();

        public override void Attack(Warrior enemy)
        {
            float damage = Damage;
            int coeffcient = 2;

            if (_isFirstAttack)
            {
                damage *= coeffcient;
                _isFirstAttack = false;
            }

            if (IsHealthLess(TenPercentHealth) && WasWhereCanse)
            {
                damage *= coeffcient;
            }

            enemy.TakeDamage(damage);
        }

        public override void TakeDamage(float damage)
        {
            if (WasWhereCanse)
            {
                DrinkHealingPotion();
            }

            base.TakeDamage(damage);
        }
    }
}
