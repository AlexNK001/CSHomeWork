using System;
using System.Collections.Generic;
using System.Linq;

namespace Amnesty
{
    public class Programm
    {
        private static void Main()
        {
            int criminalCount = 20;
            Creator creator = new Creator();

            Jail informant = new Jail(creator.GetPrisoners(criminalCount), creator.GetCrimes());
            informant.ShowCertainPrisoners();
        }
    }

    public class Jail
    {
        private readonly List<string> _crimes;
        private List<Prisoner> _prisoners;

        public Jail(List<Prisoner> prisoners, List<string> crimes)
        {
            _prisoners = prisoners;
            _crimes = crimes;
        }

        public void ShowCertainPrisoners()
        {
            const string CommandShowPrisoners = "1";
            const string CommanShowSelected = "2";
            const string CommandExit = "3";

            string crime = SelectCrime();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{CommandShowPrisoners} - Показать преступников.");
                Console.WriteLine($"{CommanShowSelected} - Амнистировать преступников по статье {crime}.");
                Console.WriteLine($"{CommandExit} - Выход.");

                switch (Console.ReadLine())
                {
                    case CommandShowPrisoners:
                        _prisoners.ForEach(criminal => criminal.ShowInfo(crime));
                        break;

                    case CommanShowSelected:
                        _prisoners = _prisoners.Where(prisoner => prisoner.Crime != crime).ToList();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine();
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private string SelectCrime()
        {
            int index = 0;

            while ((index > 0 && index <= _crimes.Count) == false)
            {
                int numeration = 1;
                Console.WriteLine("Выберете статью.");

                foreach (string crime in _crimes)
                {
                    Console.WriteLine($"{numeration} {crime}");
                    numeration++;
                }

                if (int.TryParse(Console.ReadLine(), out index) == false)
                {
                    Console.WriteLine("Некорректный ввод!");
                    Console.ReadKey();
                }

                Console.Clear();
            }

            return _crimes[--index];
        }
    }

    public class Prisoner
    {
        public Prisoner(string fullName, string crime)
        {
            Name = fullName;
            Crime = crime;
        }

        public string Name { get; }
        public string Crime { get; }

        public void ShowInfo(string crime)
        {
            if (Crime == crime)
            {
                Console.Write($"{Name} ");
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{Crime}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"{Name} {Crime}");
            }
        }
    }

    public class Creator
    {
        private readonly List<string> _names;
        private readonly List<string> _surnames;
        private readonly List<string> _middleNames;
        private readonly List<string> _crimes;

        public Creator()
        {
            _names = GetNames();
            _surnames = GetSurnames();
            _middleNames = GetMiddleNames();
            _crimes = GetCrimes();
        }

        public List<Prisoner> GetPrisoners(int count)
        {
            List<Prisoner> prisoners = new List<Prisoner>();

            for (int i = 0; i < count; i++)
            {
                prisoners.Add(GetPrisoner());
            }

            return prisoners;
        }

        public List<string> GetCrimes()
        {
            List<string> crimes = new List<string>
            {
                "Антиправительственное",
                "Кража",
                "Разбой",
                "Убийство",
                "Нарушал принципы инкапсуляции",
                "Передавал поля в методы этого же класса",
                "Не указавал доступ у полей"
            };

            return crimes;
        }

        private Prisoner GetPrisoner()
        {
            char symbol = ' ';
            string name = _names[UserUtils.GenerateRandomNumber(_names.Count)]; ;
            string surname = _surnames[UserUtils.GenerateRandomNumber(_surnames.Count)];
            string middleName = _middleNames[UserUtils.GenerateRandomNumber(_middleNames.Count)];
            string fullName = name + symbol + surname + symbol + middleName;
            string crime = _crimes[UserUtils.GenerateRandomNumber(_crimes.Count)];

            return new Prisoner(fullName, crime);
        }

        private List<string> GetNames()
        {
            List<string> names = new List<string>
            {
                "Александр",
                "Михаил",
                "Дмитрий",
                "Николай",
                "Иван",
                "Денис",
                "Антон"
            };

            return names;
        }

        private List<string> GetSurnames()
        {
            List<string> surnames = new List<string>
            {
                "Иванов",
                "Петров",
                "Семенов",
                "Попов",
                "Смирнов",
                "Кузнецов",
                "Соколов"
            };

            return surnames;
        }

        private List<string> GetMiddleNames()
        {
            List<string> midlleNames = new List<string>
            {
                "Александрович",
                "Игоревич",
                "Сергеевич",
                "Геннадьевич",
                "Дмитриевич",
                "Никитич",
                "Ильич"
            };

            return midlleNames;
        }
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int max) => s_random.Next(max);
    }
}
