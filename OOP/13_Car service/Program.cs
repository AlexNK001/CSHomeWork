using System;
using System.Collections.Generic;

namespace _13_Car_service
{
    public class Programm
    {
        private static void Main()
        {
            DetailFactory detailFactory = new DetailFactory();
            CarFactory creator = new CarFactory(detailFactory);
            Warehouse warehouse = new Warehouse(detailFactory);
            Service service = new Service(money: 10000, penaltyRefusal: 100, creator.CreateCars(20), detailFactory, warehouse);
            service.Work();
            Console.ReadKey();
        }
    }

    public class Service
    {
        private readonly int _penaltyRefusal;
        private readonly Queue<Car> _cars;
        private readonly Dictionary<string, DetailInfo> _detailsInfo;
        private readonly DetailFactory _detailFactory;
        private readonly Warehouse _warehouse;
        private int _money;

        public Service(int money, int penaltyRefusal, Queue<Car> cars, DetailFactory detailFactory, Warehouse warehouse)
        {
            _warehouse = warehouse;
            _detailFactory = detailFactory;
            _detailsInfo = InitializeDetailsInformation();
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

            DetailInfo detailInfo = _detailsInfo[_detailFactory[index].Name];

            if (_warehouse.TryGiveDetail(_detailFactory[index].Name, out Detail detail))
            {
                if (car.TryReplaceDetail(detail))
                {
                    _money += detailInfo.Price;
                    _money += detailInfo.ReplacementPrice;
                }
                else
                {
                    _money -= detailInfo.Price;
                    Console.WriteLine($"Вы заменили работающую деталь. Штраф: {detailInfo.Price}");
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
            IReadOnlyList<Detail> details = car.Details;
            string line;
            char symbol = '|';
            string detailCar = "Деталь автомобиля";
            string state = "Состояние";
            string goodDetail = "Исправна";
            string brokenDetail = "Сломана";
            int longestName = 20;

            Console.WriteLine(detailCar + symbol + state + "\n");

            foreach (var detail in details)
            {
                line = detail.Name.PadRight(longestName) + symbol;
                line += detail.IsBroken ? goodDetail : brokenDetail;
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
            int longestName = 20;

            string line = new string(' ', numberLength) + symbol;
            line += detail.PadRight(longestName) + symbol;
            line += price + symbol + repairCost + symbol;
            line += fineAmount + symbol + count + symbol;
            Console.WriteLine(line);

            foreach (var cell in _detailsInfo.Keys)
            {
                DetailInfo detailInfo = _detailsInfo[cell];

                line = number.ToString().PadRight(numberLength) + symbol;
                line += cell.PadRight(longestName);
                line += symbol + detailInfo.Price.ToString().PadLeft(price.Length);
                line += symbol + detailInfo.ReplacementPrice.ToString().PadLeft(repairCost.Length);
                line += symbol + detailInfo.Fine.ToString().PadLeft(fineAmount.Length) + symbol;
                line += _warehouse.CountCurrentDetail(cell).ToString().PadLeft(count.Length) + symbol;
                Console.WriteLine(line);

                number++;
            }

            Console.WriteLine($"Для отказа клиенту нажмите {refusalNumber}. Размер штрафа: {_penaltyRefusal}");
        }

        private bool TrySelectAction(out int index)
        {
            if (int.TryParse(Console.ReadLine(), out index))
            {
                if (index >= 0 && index <= _detailFactory.DetailsCount)
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

        private Dictionary<string, DetailInfo> InitializeDetailsInformation()
        {
            int[] pricies = new int[] { 1500, 750, 200, 360, 120 };
            int[] repairs = new int[] { 600, 500, 50, 250, 80 };
            int[] fines = new int[] { 1600, 900, 250, 410, 190 };

            Dictionary<string, DetailInfo> detailsInfo = new Dictionary<string, DetailInfo>();

            for (int i = 0; i < _detailFactory.DetailsCount; i++)
            {
                string name = _detailFactory[i].Name;
                detailsInfo.Add(name, new DetailInfo(name, pricies[i], repairs[i], fines[i]));
            }

            return detailsInfo;
        }
    }

    public class Warehouse
    {
        private readonly Dictionary<string, Cell> _details;

        public Warehouse(DetailFactory detailFactory)
        {
            int minNumber = 10;
            int maxNUmber = 20;
            _details = new Dictionary<string, Cell>();

            for (int i = 0; i < detailFactory.DetailsCount; i++)
            {
                string name = detailFactory[i].Name;
                _details.Add(name, new Cell(detailFactory[i], UserUtils.GenerateRandomNumber(minNumber, maxNUmber)));
            }
        }

        public int CountCurrentDetail(string nameDetail)
        {
            return _details.ContainsKey(nameDetail) ? _details[nameDetail].Count : -1;
        }

        public bool TryGiveDetail(string detailName, out Detail detail)
        {
            if (_details.ContainsKey(detailName))
            {
                return _details[detailName].TryGiveDetail(out detail);
            }

            detail = null;
            return false;
        }
    }

    public class DetailInfo
    {
        public DetailInfo(string detailName, int price, int repair, int fine)
        {
            DetailName = detailName;
            Price = price;
            ReplacementPrice = repair;
            Fine = fine;
        }

        public int Price { get; }
        public int ReplacementPrice { get; }
        public int Fine { get; }
        public string DetailName { get; }
    }

    public class Cell
    {
        private readonly Detail _detail;

        public Cell(Detail detail, int count)
        {
            _detail = detail;
            Count = count;
        }

        public int Count { get; private set; }

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
        private readonly List<Detail> _details;

        public Car(string name, List<Detail> details)
        {
            Name = name;
            _details = details;
        }

        public string Name { get; }
        public IReadOnlyList<Detail> Details => _details;

        public bool TryReplaceDetail(Detail detail)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Name == detail.Name)
                {
                    _details[i] = detail.Clone();

                    return HaveBrokenParts();
                }
            }

            Console.WriteLine("Автомобиль не имеет данной детали!");
            return false;
        }

        private bool HaveBrokenParts()
        {
            foreach (Detail detail in _details)
            {
                if (detail.IsBroken == false)
                    return false;
            }

            return true;
        }
    }

    public class CarFactory
    {
        private readonly DetailFactory _detailFactory;
        private readonly List<string> _brands;

        public CarFactory(DetailFactory detailFactory)
        {
            _detailFactory = detailFactory;
            _brands = CreateBrands();
        }

        public Queue<Car> CreateCars(int count)
        {
            Queue<Car> cars = new Queue<Car>();
            string brand;

            for (int i = 0; i < count; i++)
            {
                brand = _brands[UserUtils.GenerateRandomNumber(_brands.Count)];
                cars.Enqueue(new Car(brand, _detailFactory.CreateBreakDetails()));
            }

            return cars;
        }

        private List<string> CreateBrands()
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

    public class DetailFactory
    {
        private readonly List<Detail> _details;

        public DetailFactory()
        {
            _details = Initialize();
        }

        public Detail this[int detailNumber] => _details[detailNumber];
        public int DetailsCount => _details.Count;

        public List<Detail> CreateBreakDetails()
        {
            int number = UserUtils.GenerateRandomNumber(_details.Count);

            List<Detail> details = new List<Detail>();

            for (int i = 0; i < _details.Count; i++)
                details.Add(_details[i].Clone(i != number));

            return details;
        }

        private List<Detail> Initialize()
        {
            List<Detail> details = new List<Detail>
            {
                new Detail("Engine"),
                new Detail("Gearbox"),
                new Detail("Generator"),
                new Detail("CoolingSystem"),
                new Detail("AirSystem")
            };

            return details;
        }
    }

    public class Detail
    {
        public Detail(string name, bool IsBroken = true)
        {
            Name = name;
            this.IsBroken = IsBroken;
        }

        public string Name { get; private set; }
        public bool IsBroken { get; private set; }

        public Detail Clone(bool isBroken = true)
        {
            return new Detail(Name, isBroken);
        }
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
