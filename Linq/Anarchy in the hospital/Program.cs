using System;
using System.Collections.Generic;
using System.Linq;

namespace Anarchy_in_the_hospital
{
    public class Programm
    {
        private static void Main()
        {
            Creator creator = new Creator();
            Hospital hospital = new Hospital(creator.CreatePatients());
            hospital.ViewPatients();
        }
    }

    public class Hospital
    {
        private const string CommandSortName = "1";
        private const string CommandSortAge = "2";
        private const string CommandShowDisease = "3";
        private const string CommandExit = "4";

        private readonly List<Patient> _patients;

        public Hospital(List<Patient> patients)
        {
            _patients = patients;
        }
        
        public void ViewPatients()
        {
            bool isWork = true;

            while (isWork)
            {
                string displayMenu = $"{CommandSortName} - Отсортировать всех больных по фио";
                displayMenu += $"{CommandSortAge} - Отсортировать всех больных по возрасту\n";
                displayMenu += $"{CommandShowDisease} - Вывести больных с определенным заболеванием\n";
                displayMenu += $"{CommandExit} - Выход";
                Console.WriteLine(displayMenu);

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
            var triagedPatients = _patients.
                OrderBy(patient => patient.FullName);

            ShowPatients(triagedPatients);
        }

        private void ShowSorteredPatientsByAge()
        {
            var triagedPatients = _patients.
               OrderBy(patient => patient.Age);

            ShowPatients(triagedPatients);
        }

        private void ShowPatientsByDisease()
        {
            Console.Write("Введите название заболевания:");
            string desiredDisease = Console.ReadLine();

            if (_patients.Any(patient => patient.Disease.ToLower() == desiredDisease.ToLower()))
            {
                var selectedPatients = _patients.
                    Where(patient => patient.Disease.ToLower() == desiredDisease.ToLower());

                ShowPatients(selectedPatients);
            }
            else
            {
                Console.WriteLine("пациент с таким заболеванием не найден");
            }
        }

        private void ShowPatients(IEnumerable<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                Console.WriteLine($"{patient.FullName.PadRight(GetMaxNameLength())} {patient.Age}");
            }
        }

        private int GetMaxNameLength()
        {
            return _patients.Max(patient => patient.FullName.Length);
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
        public List<Patient> CreatePatients()
        {
            char symbol = ' ';
            int minAge = 18;
            int maxAge = 88;
            List<Patient> patients = new List<Patient>();

            List<string> names = GetNames();
            List<string> surnames = GetSurnames();
            List<string> middleNames = GetMiddleNames();
            List<string> disease = GetDiseases();

            for (int i = 0; i < names.Count; i++)
            {
                string fullName = surnames[i] + symbol + names[i] + symbol + middleNames[i];
                int age = UserUtils.GenerateRandomNumber(minAge, maxAge);
                patients.Add(new Patient(fullName, age, disease[i]));
            }

            return patients;
        }

        private List<string> GetNames()
        {
            return new List<string>
            {
                "Александр",
                "Михаил",
                "Дмитрий",
                "Николай",
                "Иван",
                "Денис",
                "Антон",
                "Пётр",
                "Алексей",
                "Евгений"
            };
        }

        private List<string> GetSurnames()
        {
            return new List<string>
            {
                "Иванов",
                "Петров",
                "Семенов",
                "Попов",
                "Смирнов",
                "Кузнецов",
                "Соколов",
                "Коздов",
                "Рыбаков",
                "Гуляков"
            };
        }

        private List<string> GetMiddleNames()
        {
            return new List<string>
            {
                "Александрович",
                "Игоревич",
                "Сергеевич",
                "Геннадьевич",
                "Дмитриевич",
                "Никитич",
                "Ильич",
                "Иванович",
                "Петрович",
                "Евгенивич"
            };
        }

        private List<string> GetDiseases()
        {
            return new List<string>
            {
                "Гастрит",
                "Язва желудка",
                "Простатит",
                "Геморрой",
                "Гломерулонефрит",
                "Пиелонефрит",
                "Бронхит",
                "Халера",
                "Чума",
                "Оспа"
            };
        }
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }
    }
}
