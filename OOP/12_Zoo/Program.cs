using System;
using System.Collections.Generic;

namespace _12_Zoo
{
    public class ZooHomeWork
    {
        static void Main(string[] args)
        {
            Zoo zoopark = new Zoo();
            zoopark.ShowEnclosures();
        }
    }

    public class Zoo
    {
        private List<Animal> _animals;
        private Dictionary<Type, string> _sings;
        private List<Aviary> _enclosures;

        public Zoo()
        {
            _animals = GetAnimals();
            _sings = GetSings();
            _enclosures = DistributeAnimals(_animals);
        }

        public void ShowEnclosures()
        {
            bool isOpen = true;
            int exitNumber = 0;
            int minNumber = 1;

            while (isOpen)
            {
                Console.WriteLine($"Выберите вольер от {minNumber} до {_enclosures.Count}");
                Console.WriteLine($"Для выхода нажмите {exitNumber}");

                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0 && value <= _enclosures.Count)
                    {
                        value--;
                        _enclosures[value].ShowInfo();
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

        private List<Aviary> DistributeAnimals(List<Animal> animals)
        {
            List<Aviary> aviaries = new List<Aviary>();
            bool isWork = true;

            while (isWork)
            {
                if (animals.Count > 0)
                {
                    List<Animal> selectedAnimals = DivideAnimals();
                    aviaries.Add(FillEnclosure(selectedAnimals));
                }
                else
                {
                    isWork = false;
                }
            }

            return aviaries;
        }

        private Aviary FillEnclosure(List<Animal> animals)
        {
            string name;
            Type type = animals[0].GetType();

            if (_sings.ContainsKey(type))
            {
                name = _sings[type];
            }
            else
            {
                name = "Табличка для данного вольёра времено отсутсвует.";
            }

            Aviary aviary = new Aviary(name, animals);

            return aviary;
        }

        private List<Animal> DivideAnimals()
        {
            List<Animal> finalAnimals = new List<Animal>();
            Animal currentAnimal = _animals[_animals.Count - 1];

            for (int i = _animals.Count - 1; i >= 0; i--)
            {
                if (_animals[i].GetType() == currentAnimal.GetType())
                {
                    finalAnimals.Add(_animals[i]);
                    _animals.Remove(_animals[i]);
                }
            }

            _animals.Remove(currentAnimal);

            return finalAnimals;
        }

        private Dictionary<Type, string> GetSings()
        {
            Dictionary<Type, string> sings = new Dictionary<Type, string>
            {
                { typeof(Badger), "В этом вольере проживают Барсуки." },
                { typeof(Bear), "В этом вольере проживают Медведи." },
                { typeof(Skunk), "В этом вольере проживают Скунсы." },
                { typeof(Beaver), "В этом вольере проживают Бобры." }
            };

            return sings;
        }

        private List<Animal> GetAnimals()
        {
            List<Animal> list = new List<Animal>
            {
                new Badger(Gender.Male),
                new Badger(Gender.Female),
                new Bear(Gender.Female),
                new Bear(Gender.Male),
                new Skunk(Gender.Male),
                new Skunk(Gender.Female),
                new Beaver(Gender.Female),
                new Beaver(Gender.Male),
                new Badger(Gender.Male),
                new Bear(Gender.Female),
                new Skunk(Gender.Male),
                new Beaver(Gender.Female),
                new Badger(Gender.Male),
                new Bear(Gender.Female),
                new Skunk(Gender.Male),
                new Beaver(Gender.Female),
                new Badger(Gender.Male),
                new Bear(Gender.Female),
                new Skunk(Gender.Male),
                new Beaver(Gender.Female),
            };

            return list;
        }
    }

    public class Aviary
    {
        private string _name;
        private List<Animal> _animals;

        public Aviary(string name, List<Animal> animals)
        {
            _name = name;
            _animals = animals;
        }

        public void ShowInfo()
        {
            int countMale = GetNumberMale();
            int countFemale = _animals.Count - countMale;

            Console.WriteLine($"Вольер: {_name}");
            Console.WriteLine($"Всего животных: {_animals.Count}");
            Console.WriteLine($"Самок: {countFemale}");
            Console.WriteLine($"Самцов: {_animals.Count - countFemale}");
            Console.WriteLine($"Звук который они издают: {_animals[0].Sound}\n");
        }

        private int GetNumberMale()
        {
            int countMale = 0;

            foreach (Animal animal in _animals)
            {
                if (animal.Gender == Gender.Male)
                {
                    countMale++;
                }
            }

            return countMale;
        }
    }

    public abstract class Animal
    {
        private Gender _gender;
        private string _sound;

        public Animal(Gender gender, string sound)
        {
            _gender = gender;
            _sound = sound;
        }

        public Gender Gender => _gender;
        public string Sound => _sound;
    }

    public class Badger : Animal
    {
        public Badger(
            Gender gender,
            string sound = "ААААррррхххх!!!") : base(gender, sound) { }
    }

    public class Bear : Animal
    {
        public Bear(
            Gender gender,
            string sound = "Аррррр") : base(gender, sound) { }
    }

    public class Skunk : Animal
    {
        public Skunk(
            Gender gender,
            string sound = "пук-пук") : base(gender, sound) { }
    }

    public class Beaver : Animal
    {
        public Beaver(
            Gender gender,
            string sound = "Уаааа") : base(gender, sound) { }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
