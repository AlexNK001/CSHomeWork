using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Car_service
{
    public class Programm
    {
        static void Main()
        {
            CarFactory creator = new CarFactory();
            Service service = new Service(10000, 100, creator.CreateCars(20));
            service.Work();
            Console.ReadKey();
        }
    }

    public class Service
    {
        private int _money;
        private List<Cell> _cells;
        private int _penaltyRefusal;
        private Queue<Car> _cars;

        public Service(int money, int penaltyRefusal, Queue<Car> cars)
        {
            _cells = GetCells();
            _money = money;
            _penaltyRefusal = penaltyRefusal;
            _cars = cars;
        }

        public void Work()
        {
            while (_cars.Count > 0)
            {
                Console.WriteLine($"Счёт сервиса: {_money} Количество клиентов: {_cars.Count}");
                Car car = _cars.Dequeue();
                ShowStateCar(car);
                ShowSelectionScreen();

                if (TrySelectAction(out int index))
                {
                    if (index == 0)
                    {
                        _money -= _penaltyRefusal;
                        Console.WriteLine($"Штраф за отказ клиенту: {_penaltyRefusal}");
                    }
                    else
                    {
                        RepairCar(car, index);
                    }
                }
                else
                {
                    Console.WriteLine("Неправильный выбор команды");
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void RepairCar(Car car, int index)
        {
            index--;
            Cell cell = _cells[index];

            if (cell.TryGiveDetail(out Detail detail))
            {
                if (car.TryReplaceDetail(detail))
                {
                    _money += cell.Price;
                    _money += cell.ReplacementPrice;
                }
                else
                {
                    _money -= cell.Price;
                    Console.WriteLine($"Вы заменили работающую деталь. Штраф: {cell.Price}");
                }
            }
            else
            {
                _money -= _penaltyRefusal;
                Console.WriteLine($"Детали нет на складе. Штраф: {_penaltyRefusal}");
            }
        }

        private void ShowStateCar(Car car)
        {
            Console.WriteLine($"Автомобиль: {car.Name}");
            Dictionary<string, bool> condition = car.GetConditionDetails();
            string line;
            char symbol = '|';
            string detail = "Деталь автомобиля";
            string state = "Состояние";
            string goodDetail = "Исправна";
            string brokenDetail = "Сломана";
            int longestName = FindLongestName(condition, detail);

            Console.WriteLine(detail + symbol + state + "\n");

            foreach (string name in condition.Keys)
            {
                line = name.PadRight(longestName) + symbol;
                line += condition[name] ? goodDetail : brokenDetail;
                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private void ShowSelectionScreen()
        {
            int numberLength = 2;
            int number = 1;
            int refusalNumber = 0;
            char symbol = '|';
            string detail = "Деталь";
            string price = "Цена детали";
            string repairCost = "Стоимость ремонта";
            string fineAmount = "Размер штрафа";
            string count = "Количество";
            int longestName = FindLongestName(detail);

            string line = new string(' ', numberLength) + symbol;
            line += detail.PadRight(longestName) + symbol;
            line += price + symbol + repairCost + symbol;
            line += fineAmount + symbol + count + symbol;
            Console.WriteLine(line);

            foreach (Cell cell in _cells)
            {
                line = number.ToString().PadRight(numberLength) + symbol;
                line += cell.DetailName.PadRight(longestName);
                line += symbol + cell.Price.ToString().PadLeft(price.Length);
                line += symbol + cell.ReplacementPrice.ToString().PadLeft(repairCost.Length);
                line += symbol + cell.Fine.ToString().PadLeft(fineAmount.Length) + symbol;
                line += cell.Count.ToString().PadLeft(count.Length) + symbol;
                Console.WriteLine(line);

                number++;
            }

            Console.WriteLine($"Для отказа клиенту нажмите {refusalNumber}. Размер штрафа: {_penaltyRefusal}");
        }

        private int FindLongestName(Dictionary<string, bool> condition, string text)
        {
            int number = text.Length;

            foreach (string name in condition.Keys)
            {
                if (name.Length > number)
                    number = name.Length;
            }

            return number;
        }

        private int FindLongestName(string text)
        {
            int number = text.Length;

            foreach (Cell detail in _cells)
            {
                if (detail.DetailName.Length > number)
                    number = detail.DetailName.Length;
            }

            return number;
        }

        private bool TrySelectAction(out int index)
        {
            if (int.TryParse(Console.ReadLine(), out index))
            {
                if (index >= 0 && index <= _cells.Count)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Детали с таким номером нет!");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод!");
            }

            return false;
        }

        private List<Cell> GetCells()
        {
            List<Cell> cell = new List<Cell>
            {
                new Cell(new Engine(), 1500, 600, 1600, 1),
                new Cell(new Gearbox(), 750, 500, 900, 1),
                new Cell(new Generator(), 200, 50, 250, 1),
                new Cell(new CoolingSystem(), 360, 250, 410, 1),
                new Cell(new AirSystem(), 120, 80, 190, 1)
            };

            return cell;
        }
    }

    public class Cell
    {
        private Detail _detail;

        public Cell(Detail detail, int price, int repair, int fine, int count)
        {
            _detail = detail;
            Price = price;
            ReplacementPrice = repair;
            Fine = fine;
            Count = count;
        }

        public int Price { get; }
        public int ReplacementPrice { get; }
        public int Fine { get; }
        public int Count { get; private set; }
        public string DetailName => _detail.Name;

        public bool TryGiveDetail(out Detail detail)
        {
            detail = null;

            if (Count > 0)
            {
                Count--;
                detail = _detail.Clone();
                Console.WriteLine($"Деталь ({_detail.Name}) взята из склада. Осталось {Count}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Деталь ({_detail.Name}) закончилась!");
                return false;
            }
        }
    }

    public class Car
    {
        private List<Detail> _details;

        public Car(string name, List<Detail> details)
        {
            Name = name;
            _details = details;
        }

        public string Name { get; }

        public bool TryReplaceDetail(Detail detail)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Name == detail.Name)
                {
                    _details[i] = detail.Clone();

                    return IsWorking();
                }
            }

            Console.WriteLine("Автомобиль не имеет данной детали!");
            return false;
        }

        public Dictionary<string, bool> GetConditionDetails()
        {
            Dictionary<string, bool> condition = new Dictionary<string, bool>();

            foreach (Detail detail in _details)
                condition.Add(detail.Name, detail.IsWorks);

            return condition;
        }

        private bool IsWorking()
        {
            foreach (Detail detail in _details)
            {
                if (detail.IsWorks == false)
                    return false;
            }

            return true;
        }
    }

    public class CarFactory
    {
        private List<Detail> _details;
        private List<string> _brands;

        public CarFactory()
        {
            _details = GetDetails();
            _brands = GetBrands();
        }

        public Queue<Car> CreateCars(int count)
        {
            Queue<Car> cars = new Queue<Car>();
            string brand;

            for (int i = 0; i < count; i++)
            {
                brand = _brands[UserUtils.GenerateRandomNumber(_brands.Count)];
                cars.Enqueue(new Car(brand, GetBreakDetails()));
            }

            return cars;
        }

        private List<Detail> GetBreakDetails()
        {
            int number = UserUtils.GenerateRandomNumber(_details.Count);

            List<Detail> details = new List<Detail>();

            for (int i = 0; i < _details.Count; i++)
                details.Add(_details[i].Clone(i != number));

            return details;
        }

        private List<Detail> GetDetails()
        {
            List<Detail> details = new List<Detail>
            {
                new Engine(),
                new Gearbox(),
                new Generator(),
                new CoolingSystem(),
                new AirSystem()
            };

            return details;
        }

        private List<string> GetBrands()
        {
            List<string> brands = new List<string>
            {
                "Toyota",
                "Volkswagen",
                "Ford",
                "Honda",
                "Suzuki",
                "Mercedes-Benz"
            };

            return brands;
        }
    }

    public abstract class Detail
    {
        public Detail(string name, bool isWorks = true)
        {
            Name = name;
            IsWorks = isWorks;
        }

        public string Name { get; private set; }
        public bool IsWorks { get; protected set; }

        public abstract Detail Clone(bool isWorks = true);
    }

    public class Engine : Detail
    {
        public Engine(string name = "Engine", bool isWorks = true) : base(name, isWorks) { }

        public override Detail Clone(bool isWorks = true) => new Engine(Name, isWorks);
    }

    public class Gearbox : Detail
    {
        public Gearbox(string name = "Gearbox", bool isWorks = true) : base(name, isWorks) { }

        public override Detail Clone(bool isWorks = true) => new Gearbox(Name, isWorks);
    }

    public class Generator : Detail
    {
        public Generator(string name = "Generator", bool isWorks = true) : base(name, isWorks) { }

        public override Detail Clone(bool isWorks = true) => new Generator(Name, isWorks);
    }

    public class CoolingSystem : Detail
    {
        public CoolingSystem(string name = "Cooling System", bool isWorks = true) : base(name, isWorks) { }

        public override Detail Clone(bool isWorks = true) => new CoolingSystem(Name, isWorks);
    }

    public class AirSystem : Detail
    {
        public AirSystem(string name = "Air System", bool isWorks = true) : base(name, isWorks) { }

        public override Detail Clone(bool isWorks = true) => new AirSystem(Name, isWorks);
    }

    public static class UserUtils
    {
        private static readonly Random s_random = new Random();

        public static int GenerateRandomNumber(int max) => s_random.Next(max);
        public static int GenerateRandomNumber(int min, int max) => s_random.Next(min, max);
    }
}
