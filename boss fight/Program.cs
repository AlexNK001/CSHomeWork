using System;

namespace boss_fight
{
    internal class Program
    {
        private static void Main()
        {
            const string CommandHeavenlyFlame = "1";
            const string CommandFireShield = "2";
            const string CommandCallPhoenix = "3";
            const string CommandFlameRobe = "4";

            Random random = new Random();

            int boosHealth = 300;
            int currentBoosHealth = boosHealth;
            int boosDamage = 10;
            int boosDamageRemoved = 0;
            int currentBoosDamage = 0;

            int playerHealth = 200;
            int playerDamage = 10;
            int playerArmor = 10;
            int currentHealthPlayer = playerHealth;

            int bonus = 0;
            int bonusRemoved = 0;
            int bonusRecovery = 8;
            int bonusAcrual = 10;
            int deviderShield = 2;
            int dividerDamage = 3;
            bool canShield = false;
            bool haveSummoned = false;
            bool haveFlameRobe = false;

            int summonedHealth = 32;
            int summonedMoves = 5;
            int currentSummonedMoves = summonedMoves;
            int currentSummonedHealth = 0;
            int minSumonnedAtack = 7;
            int maxSummonedAtack;
            int bonusSummonedAtack = 0;

            int mana = 50;
            int currentMana = mana;
            int manaCastShield = 10;
            int manaCastSummoned = 30;
            int manaCastFlameRobe = 20;
            int recoveryMana = 4;

            int lastCurrentBoosDamage;
            int onlyPlayerBoosDamage;
            bool isWrongCommand = false;
            bool isProgramm = true;
            bool haveNotEnoughMana = false;
            string userInput;

            char symbolInBar = ' ';
            int barLength = 70;
            int boosHealthInBar;
            int remainingBoosHeathBar;
            int playerHealthInBar;
            int remainingPlayerHealthBar;
            int playerManaInBar;
            int remainingPlayerManaBar;
            int healthAtTheTimeOfTheCall;
            int summonedHealthInBar;
            int remainingSummonedHealthBar;

            Console.WindowWidth = barLength;
            Console.WindowHeight = 45;

            while (isProgramm)
            {
                lastCurrentBoosDamage = currentBoosDamage;

                if (isWrongCommand == false)
                {
                    currentBoosDamage = boosDamage + random.Next(5, boosDamage);
                }
                else
                {
                    currentBoosDamage = lastCurrentBoosDamage;
                    isWrongCommand = false;
                }

                Console.WriteLine("======================================================================");

                Console.WriteLine($" Здоровье боса {currentBoosHealth}/{boosHealth}");

                boosHealthInBar = barLength * currentBoosHealth / boosHealth;
                remainingBoosHeathBar = barLength - boosHealthInBar;

                Console.BackgroundColor = ConsoleColor.DarkRed;

                for (int j = 0; j < boosHealthInBar; j++)
                    Console.Write(symbolInBar);

                Console.BackgroundColor = ConsoleColor.DarkGray;

                for (int j = 0; j < remainingBoosHeathBar; j++)
                    Console.Write(symbolInBar);

                Console.ResetColor();

                Console.WriteLine("\n======================================================================");

                Console.WriteLine($" Ваше здоровье {currentHealthPlayer}/{playerHealth}");

                playerHealthInBar = barLength * currentHealthPlayer / playerHealth;
                remainingPlayerHealthBar = barLength - playerHealthInBar;

                Console.BackgroundColor = ConsoleColor.DarkRed;

                for (int j = 0; j < playerHealthInBar; j++)
                    Console.Write(symbolInBar);

                Console.BackgroundColor = ConsoleColor.DarkGray;

                for (int j = 0; j < remainingPlayerHealthBar; j++)
                    Console.Write(symbolInBar);

                Console.ResetColor();

                Console.WriteLine($"\n Ваша мана {currentMana}/{mana}");

                playerManaInBar = barLength * currentMana / mana;
                remainingPlayerManaBar = barLength - playerManaInBar;

                Console.BackgroundColor = ConsoleColor.DarkBlue;

                for (int j = 0; j < playerManaInBar; j++)
                    Console.Write(symbolInBar);

                Console.BackgroundColor = ConsoleColor.DarkGray;

                for (int j = 0; j < remainingPlayerManaBar; j++)
                    Console.Write(symbolInBar);

                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\n{bonus} - Заряд Огненой Урны.");

                if (haveFlameRobe == true)
                    Console.WriteLine("Пламенное Одеяние активно.");

                Console.ResetColor();

                Console.WriteLine("\n======================================================================");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{CommandHeavenlyFlame}] - Небесное пламя.");
                Console.ResetColor();
                Console.WriteLine(" Наносит фиксированую атаку пламенем и наполняеет огненную урну." +
                    "\n Может быть улучшенно Пламенным Одеянием.");
                Console.WriteLine($"  {playerDamage} - урон.");
                Console.WriteLine($"  {bonusRecovery} - заряд.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{CommandFireShield}] - Огненный щит.");
                Console.ResetColor();
                Console.WriteLine(" Поглащает часть урона, при наличии маны отражает пропущенный урон,\n увеличивая его. \n Может быть улучшенно Пламенным Одеянием");
                Console.WriteLine($"  {playerArmor} - защита.");
                Console.WriteLine($"  {dividerDamage} - множетель возврата.");
                Console.WriteLine($"  {manaCastShield} - затраты маны.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;

                if (haveSummoned == true)
                    Console.WriteLine($"[{CommandCallPhoenix}] - Самопожертвование Звездного Феникса.");
                else
                    Console.WriteLine($"[{CommandCallPhoenix}] - Призыв Звездного Феникса.");

                Console.ResetColor();
                Console.WriteLine(" Призыв помощника, который делит с вами получаемый урон пополам." +
                    "\n Здоровье Феникса зависит от недостатка вашего здоровья.\n" +
                    " Добавляет к вашим атакам случайный урон в зависимости от заряда Урны,\n обнуляя его." +
                    "\n Повторный призыв убивает помощника, востанавливая жизни игроку.");
                Console.WriteLine($"  {summonedMoves} - количество ходов.");
                Console.WriteLine($"  {manaCastSummoned} - затраты маны.");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{CommandFlameRobe}] - Пламенное Одеяние");
                Console.ResetColor();
                Console.WriteLine(" Полное уклонение от атаки. Усиливает на один ход Огненный щит \n или Небесное пламя." +
                    "Увеличавает запяды Урны.\n Повторное использование даёт полное уклоненение без затраты маны \n и снимает усиливание.");
                Console.WriteLine($"  {bonusAcrual} - заряд урны.");
                Console.WriteLine($"  {manaCastFlameRobe} - затраты маны.");

                if (haveSummoned == true)
                {
                    Console.WriteLine("======================================================================");

                    maxSummonedAtack = minSumonnedAtack + bonus;
                    bonusSummonedAtack = random.Next(minSumonnedAtack, maxSummonedAtack);

                    healthAtTheTimeOfTheCall = summonedHealth + playerHealth - currentHealthPlayer;

                    Console.WriteLine($"{currentSummonedMoves} - количество раундов призывника");
                    Console.WriteLine($" Здоровье Звездного Феникса {currentSummonedHealth}/{healthAtTheTimeOfTheCall}");

                    summonedHealthInBar = barLength * currentSummonedHealth / healthAtTheTimeOfTheCall;
                    remainingSummonedHealthBar = barLength - summonedHealthInBar;

                    Console.BackgroundColor = ConsoleColor.DarkRed;

                    for (int j = 0; j < summonedHealthInBar; j++)
                        Console.Write(symbolInBar);

                    Console.BackgroundColor = ConsoleColor.DarkGray;

                    for (int j = 0; j < remainingSummonedHealthBar; j++)
                        Console.Write(symbolInBar);

                    Console.ResetColor();
                }

                Console.WriteLine("\n======================================================================");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandHeavenlyFlame:
                        if (haveFlameRobe == true)
                        {
                            currentBoosHealth -= bonus;
                            bonus = bonusRemoved;
                            haveFlameRobe = false;
                        }

                        if (haveSummoned == true)
                        {
                            currentBoosHealth -= bonusSummonedAtack;
                        }

                        currentBoosHealth -= playerDamage;
                        bonus += bonusRecovery;
                        break;

                    case CommandFireShield:
                        if (currentMana >= manaCastShield)
                        {
                            if (currentBoosDamage > playerArmor)
                            {
                                currentBoosHealth -= (currentBoosDamage - playerArmor) * deviderShield;
                                currentMana -= manaCastShield;
                            }
                            else
                            {
                                currentMana -= manaCastShield;
                            }

                            canShield = true;
                        }
                        else
                        {
                            haveNotEnoughMana = true;
                        }

                        if (haveFlameRobe == true)
                        {
                            currentHealthPlayer += bonus;
                            bonus = bonusRemoved;
                            haveFlameRobe = false;

                            if (currentHealthPlayer > playerHealth)
                            {
                                currentHealthPlayer = playerHealth;
                            }
                        }
                        break;

                    case CommandCallPhoenix:
                        if (currentMana >= manaCastSummoned)
                        {
                            if (haveSummoned == false)
                            {
                                haveSummoned = true;
                                currentSummonedHealth = summonedHealth + playerHealth - currentHealthPlayer;
                                currentMana -= manaCastSummoned;
                                bonus = bonusRemoved;
                            }
                            else
                            {
                                haveSummoned = false;
                                currentHealthPlayer += currentSummonedHealth / dividerDamage;
                            }
                        }
                        else
                        {
                            haveNotEnoughMana = true;
                        }
                        break;

                    case CommandFlameRobe:
                        if (currentMana >= manaCastFlameRobe)
                        {
                            if (haveFlameRobe == true)
                            {
                                currentBoosDamage = boosDamageRemoved;
                                haveFlameRobe = false;
                            }
                            else if (currentMana >= manaCastFlameRobe)
                            {
                                currentBoosDamage = boosDamageRemoved;
                                currentMana -= manaCastFlameRobe;
                                haveFlameRobe = true;
                                bonus += bonusAcrual;
                            }
                        }
                        else
                        {
                            haveNotEnoughMana = true;
                        }
                        break;

                    default:
                        isWrongCommand = true;
                        break;
                }

                if (haveNotEnoughMana == true)
                {
                    Console.WriteLine("Не хватает маны!");
                    haveNotEnoughMana = false;
                    Console.ReadKey();
                }

                Console.Clear();

                if (isWrongCommand == false)
                {
                    onlyPlayerBoosDamage = currentBoosDamage;

                    if (haveSummoned == true)
                        onlyPlayerBoosDamage /= dividerDamage;

                    if (canShield == true)
                    {
                        onlyPlayerBoosDamage -= playerArmor;
                        canShield = false;
                    }

                    currentHealthPlayer -= onlyPlayerBoosDamage;

                    if (currentMana < mana)
                    {
                        currentMana += recoveryMana;

                        if (currentMana > mana)
                            currentMana = mana;
                    }

                    if (haveSummoned == true)
                    {
                        currentSummonedHealth -= currentBoosDamage / dividerDamage;

                        if (currentSummonedMoves <= 0 || currentSummonedHealth <= 0)
                            haveSummoned = false;

                        currentSummonedMoves--;
                    }

                    if (currentHealthPlayer <= 0 || currentBoosHealth <= 0)
                    {
                        isProgramm = false;
                    }
                }
            }

            if (currentHealthPlayer <= 0 && currentBoosHealth <= 0)
                Console.WriteLine("Вы убили друг друга");

            if (currentHealthPlayer <= 0)
                Console.WriteLine("Вы погибли");

            if (currentBoosHealth <= 0)
                Console.WriteLine("Вы победили");

            Console.ReadKey();
        }
    }
}
