using System;
using System.Collections.Generic;

namespace _12_Zoo
{
    public class ZooHomeWork
    {
        private static void Main()
        {
            ConfigsStorage animalList = new ConfigsStorage();

            Farm animalCreator = new Farm();
            CageFactory aviaryCreator = new CageFactory();

            List<Aviary> aviaries = aviaryCreator.Create(animalList, animalCreator);
            Zoo zoo = new Zoo(aviaries);

            zoo.ShowEnclosures();
        }
    }

    public class Zoo
    {
        private readonly List<Aviary> _enclosures;

        public Zoo(List<Aviary> aviaries)
        {
            _enclosures = aviaries;
        }

        public void ShowEnclosures()
        {
            bool isOpen = true;
            int exitNumber = 0;

            while (isOpen)
            {
                ShowMainMenu(_enclosures, exitNumber);

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0 && value <= _enclosures.Count)
                    {
                        value--;
                        ShowAviaryInfo(_enclosures[value]);
                    }
                    else if (value == exitNumber)
                    {
                        isOpen = false;
                    }
                    else
                    {
                        Console.WriteLine("Вольера с таким номер нет.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод.");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowMainMenu(IReadOnlyList<Aviary> aviaries, int exitNumber)
        {
            int numberToConvertIndex = 1;
            string finalText = $"Выберите вольер\n";
            finalText += $"Для выхода нажмите {exitNumber}\n";

            for (int i = 0; i < aviaries.Count; i++)
            {
                finalText += $"{i + numberToConvertIndex}) {aviaries[i].Name}\n";
            }

            Console.WriteLine(finalText);
        }

        private void ShowAviaryInfo(Aviary aviary)
        {
            string finalText = $"Вольер: {aviary.Name}";
            finalText += $"Всего животных: {aviary.CountMale + aviary.CountFemale}\n";
            finalText += $"Самок: {aviary.CountFemale}\n";
            finalText += $"Самцов: {aviary.CountMale}\n";
            finalText += $"Звук который они издают: {aviary.Sound}";

            Console.WriteLine(finalText);
        }
    }

    public class ConfigsStorage
    {
        private readonly List<Config> _configs;

        public ConfigsStorage()
        {
            _configs = Initialize();
        }

        public Config this[int index] => _configs[index];
        public int AnimalCount => _configs.Count;

        private List<Config> Initialize()
        {
            return new List<Config>
            {
                { new Config("Барсук","ААААррррхххх!!!", "В этом вольере проживают Барсуки.") },
                { new Config("Медведь","Аррррр", "В этом вольере проживают Медведи.") },
                { new Config("Скунс","пук-пук","В этом вольере проживают Скунсы.") },
                { new Config("Бобёр","Уаааа", "В этом вольере проживают Бобры.") }
            };
        }
    }

    public struct Config
    {
        public readonly AnimalConfig AnimalConfig;
        public readonly AviaryConfig AviaryConfig;

        public Config(string animalName, string animalSound, string nameSign)
        {
            AnimalConfig = new AnimalConfig(animalName, animalSound);
            AviaryConfig = new AviaryConfig(nameSign);
        }
    }

    public class Farm
    {
        public List<Animal> Create(AnimalConfig config)
        {
            int min = 10;
            int max = 20;
            List<Animal> animals = new List<Animal>();
            int animalCount = UserUtils.GenerateRandomNumber(min, max);

            for (int i = 0; i < animalCount; i++)
            {
                animals.Add(new Animal(config, GetRandomGender()));
            }

            return animals;
        }

        private Gender GetRandomGender()
        {
            int percent = 100;
            int percentageChoice = 50;
            int result = UserUtils.GenerateRandomNumber(percent);
            return result >= percentageChoice ? Gender.Male : Gender.Female;
        }
    }

    public class Animal
    {
        public Animal(AnimalConfig config, Gender gender)
        {
            Name = config.Name;
            Sound = config.Sound;
            Gender = gender;
        }

        public string Name { get; }
        public string Sound { get; }
        public Gender Gender { get; }
    }

    public struct AnimalConfig
    {
        public AnimalConfig(string name, string sound)
        {
            Name = name;
            Sound = sound;
        }

        public string Name { get; }
        public string Sound { get; }
    }

    public class CageFactory
    {
        public List<Aviary> Create(ConfigsStorage configsStorage, Farm farm)
        {
            List<Aviary> aviaries = new List<Aviary>();

            for (int i = 0; i < configsStorage.AnimalCount; i++)
            {
                Config config = configsStorage[i];
                Aviary aviary = new Aviary(config.AviaryConfig, farm.Create(config.AnimalConfig));
                aviaries.Add(aviary);
            }

            return aviaries;
        }
    }

    public class Aviary
    {
        private readonly List<Animal> _animals;

        public Aviary(AviaryConfig aviaryConfig, List<Animal> animals)
        {
            Name = aviaryConfig.NameSign;
            _animals = animals;
            CountAnimalsByGender();
        }

        public string Name { get; }
        public int CountMale { get; private set; }
        public int CountFemale { get; private set; }
        public string Sound => _animals[0].Sound;

        private void CountAnimalsByGender()
        {
            foreach (var item in _animals)
            {
                switch (item.Gender)
                {
                    case Gender.Male:
                        CountMale++;
                        break;

                    case Gender.Female:
                        CountFemale++;
                        break;
                }
            }
        }
    }

    public struct AviaryConfig
    {
        public AviaryConfig(string nameSign)
        {
            NameSign = nameSign;
        }

        public string NameSign { get; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return s_random.Next(min, max);
        }

        public static int GenerateRandomNumber(int max)
        {
            return s_random.Next(max);
        }
    }
}
