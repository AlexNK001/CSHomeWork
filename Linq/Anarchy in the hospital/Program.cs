using System;
using System.Collections.Generic;
using System.Linq;

namespace Anarchy_in_the_hospital
{
    public class Programm
    {
        private static void Main()
        {
            int countPatients = 50;
            Creator creator = new Creator();
            Hospital hospital = new Hospital(creator.GetPatients(countPatients));
            hospital.ViewPatients();
        }
    }

    public class Hospital
    {
        private readonly List<Patient> _patients;
        private readonly Dictionary<int, string> _ageLimits;

        public Hospital(List<Patient> patients)
        {
            _patients = patients;
            _ageLimits = GetAgeLimits();
        }

        public int MaxNameLength => _patients.Max(patient => patient.FullName.Length);

        public void ViewPatients()
        {
            const string CommandSortName = "1";
            const string CommandSortAge = "2";
            const string CommandShowDisease = "3";
            const string CommandExit = "4";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{CommandSortName} - Отсортировать всех больных по фио");
                Console.WriteLine($"{CommandSortAge} - Отсортировать всех больных по возрасту");
                Console.WriteLine($"{CommandShowDisease} - Вывести больных с определенным заболеванием");
                Console.WriteLine($"{CommandExit} - Выход");

                switch (Console.ReadLine())
                {
                    case CommandSortName:
                        ShowSortedPatientsByName();
                        break;

                    case CommandSortAge:
                        ShowSorteredPatientsByAge();
                        break;

                    case CommandShowDisease:
                        ShowPatientsByDisease();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowSortedPatientsByName()
        {
            char symbol = '\0';
            var triagedPatients = _patients.
                OrderBy(patient => patient.FullName);

            foreach (Patient patient in triagedPatients)
            {
                char firstSymbol = patient.FullName.First();

                if (firstSymbol != symbol)
                {
                    symbol = firstSymbol;
                    Console.WriteLine($"{firstSymbol}");
                }

                Console.WriteLine($"{patient.FullName.PadRight(MaxNameLength)} {patient.Age} {patient.Disease}");
            }
        }

        private void ShowSorteredPatientsByAge()
        {
            int firstValue = int.MinValue;

            foreach (int age in _ageLimits.Keys)
            {
                Console.WriteLine(_ageLimits[age]);
                ShowAgeGroup(firstValue, age);
                firstValue = age;
                Console.WriteLine();
            }
        }

        private void ShowAgeGroup(int minAge, int maxAge)
        {
            var triagedPatients = _patients.
                Where(patient => minAge < patient.Age && patient.Age <= maxAge).
                OrderBy(patient => patient.Age);

            foreach (Patient patient in triagedPatients)
            {
                Console.WriteLine($"{patient.FullName.PadRight(MaxNameLength)} {patient.Age} {patient.Disease}");
            }
        }

        private void ShowPatientsByDisease()
        {
            Console.Write("Введите название заболевания:");
            string desiredDisease = Console.ReadLine();

            var selectedPatients = _patients.
                Where(patient => patient.Disease.ToLower() == desiredDisease.ToLower());

            foreach (Patient patient in selectedPatients)
            {
                Console.WriteLine($"{patient.FullName.PadRight(MaxNameLength)} {patient.Age}");
            }
        }

        private Dictionary<int, string> GetAgeLimits()
        {
            Dictionary<int, string> ageLimits = new Dictionary<int, string>
            {
                { 30, "Пациенты молодого возраста:" },
                { 60, "Пациенты среднего возраста:" },
                { int.MaxValue, "Пациенты пожилого возраста:" }
            };

            return ageLimits;
        }
    }

    public class Patient
    {
        public Patient(string fullName, int age, string disease)
        {
            FullName = fullName;
            Age = age;
            Disease = disease;
        }

        public string FullName { get; }
        public int Age { get; }
        public string Disease { get; }
    }

    public class Creator
    {
        private readonly List<string> _names;
        private readonly List<string> _surnames;
        private readonly List<string> _middleNames;
        private readonly List<string> _disease;

        public Creator()
        {
            _names = GetNames();
            _surnames = GetSurnames();
            _middleNames = GetMiddleNames();
            _disease = GetDiseases();
        }

        public List<Patient> GetPatients(int count)
        {
            List<Patient> patients = new List<Patient>();

            for (int i = 0; i < count; i++)
            {
                patients.Add(GivePatient());
            }

            return patients;
        }

        private Patient GivePatient()
        {
            char symbol = ' ';
            int minAge = 18;
            int maxAge = 88;

            string surname = _surnames[UserUtils.GenerateRandomNumber(_surnames.Count)];
            string name = _names[UserUtils.GenerateRandomNumber(_names.Count)]; ;
            string middleName = _middleNames[UserUtils.GenerateRandomNumber(_middleNames.Count)];
            string fullName = surname + symbol + name + symbol + middleName;

            int age = UserUtils.GenerateRandomNumber(minAge, maxAge);

            string disease = _disease[UserUtils.GenerateRandomNumber(_disease.Count)];

            return new Patient(fullName, age, disease);
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

        private List<string> GetDiseases()
        {
            List<string> disease = new List<string>
            {
                "Гастрит",
                "Язва желудка",
                "Простатит",
                "Геморрой",
                "Гломерулонефрит",
                "Пиелонефрит",
                "Бронхит"
            };

            return disease;
        }
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int max)
        {
            return s_random.Next(max);
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
