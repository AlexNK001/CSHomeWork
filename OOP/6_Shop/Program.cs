using System;
using System.Collections.Generic;

namespace _6_Shop
{
    public class Program
    {
        private static void Main()
        {
            const string MerchantSelectionMenu = "1";
            const string CommandSeePurchases = "2";
            const string CommandExit = "3";

            bool isWork = true;

            Player player = new Player("Игрок", 300, new List<Product>());

            Salesman vegetableSeller = new Salesman("Bob торговец овощами", new List<Product>
            {
                new Product("Капуста",14),
                new Product("Картошка",8)
            });

            Salesman fruitSeller = new Salesman("Bill торговец фруктами", new List<Product>
            {
                new Product("Банан",11),
                new Product("Груша",300),
                new Product("Яблоко",10),
                new Product("Апельсин",310),
                new Product("Ананас",30)
            });

            Salesman meatSeller = new Salesman("Max Мясник", new List<Product>
            {
            });

            Salesman fishSeller = new Salesman("Harry продавец рыбы", new List<Product>
            {
                new Product("Карп",11),
                new Product("Сельдь",6),
                new Product("Камбала",10),
            });

            Salesman spiceSeller = new Salesman("Jack продавец пряностями", new List<Product>
            {
                new Product("Соль",11),
                new Product("Перец",3),
            });

            Shop shop = new Shop(player, new Salesman[] { vegetableSeller, fruitSeller, meatSeller, fishSeller, spiceSeller });

            while (isWork)
            {
                Console.Clear();

                shop.ShowMembers();

                Console.WriteLine($"\n{MerchantSelectionMenu} - выбрать продавца.");
                Console.WriteLine($"{CommandSeePurchases} - посмотреть свои покупки.");
                Console.WriteLine($"{CommandExit} - выйти из программы.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MerchantSelectionMenu:
                        shop.StartShopping();
                        break;

                    case CommandSeePurchases:
                        player.ShowProducts();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    public class Shop
    {
        private Player _player;
        private Salesman[] _salesmans;
        private readonly List<Human> _members = new List<Human>();

        public Shop(Player player, Salesman[] salesmans)
        {
            _player = player;
            _salesmans = salesmans;
            _members.Add(_player);
            _members.AddRange(_salesmans);
        }

        public void StartShopping()
        {
            if (ChooseSalesman(out Salesman salesman))
            {
                ChooseProduct(salesman);
            }
        }

        public void ShowMembers()
        {
            foreach (Human members in _members)
            {
                members.ShowWhatHas();
            }
        }

        public bool ChooseSalesman(out Salesman salesman)
        {
            int salesmanNumber = 0;
            salesman = null;

            for (int i = 0; i < _salesmans.Length; i++)
            {
                if (_salesmans[i].HaveProducts)
                {
                    salesmanNumber++;
                    Console.WriteLine($"{salesmanNumber} - {_salesmans[i].Name}");
                }
            }

            if (int.TryParse(Console.ReadLine(), out int findNumber))
            {
                salesmanNumber = 0;

                for (int i = 0; i < _salesmans.Length; i++)
                {
                    if (_salesmans[i].HaveProducts)
                    {
                        salesmanNumber++;

                        if (salesmanNumber == findNumber)
                        {
                            salesman = _salesmans[i];
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void ChooseProduct(Salesman salesman)
        {
            salesman.ShowProducts();

            if (salesman.ProvideProduct(out Product product))
            {
                if (_player.BuyProducts(product))
                {
                    salesman.SellProduct(product);
                }
            }
        }
    }

    public abstract class Human
    {
        protected int Money;
        protected List<Product> Products;

        public Human(string name, int money, List<Product> products)
        {
            Name = name;
            Money = money;
            Products = products;
        }

        public string Name { get; private set; }
        public bool HaveProducts => Products.Count > 0;

        public void ShowProducts()
        {
            if (HaveProducts)
            {
                int numberProduct = 0;

                for (int i = 0; i < Products.Count; i++)
                {
                    numberProduct++;
                    Console.WriteLine($"{numberProduct}) {Products[i].Name}");
                }
            }
            else
            {
                Console.WriteLine("Продуктов нет.");
            }
        }

        public void ShowWhatHas()
        {
            Console.WriteLine($"{Name} {Money} денег, {Products.Count} товаров.");
        }
    }

    public class Player : Human
    {
        public Player(string name, int money, List<Product> products) : base(name, money, products) { }

        public bool BuyProducts(Product product)
        {
            if (IsEnoughMoney(product))
            {
                Products.Add(product);
                Money -= product.Price;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsEnoughMoney(Product product)
        {
            return Money >= product.Price && product.Price > 0;
        }
    }

    public class Salesman : Human
    {
        public Salesman(string name, List<Product> products, int money = 0) : base(name, money, products) { }

        public void SellProduct(Product product)
        {
            Products.Remove(product);
            Money += product.Price;
        }

        public bool ProvideProduct(out Product product)
        {
            product = null;

            Console.WriteLine("Введите номер товара который хотите купить.");

            if (int.TryParse(Console.ReadLine(), out int result))
            {
                result--;

                if (result < Products.Count && result >= 0)
                {
                    product = Products[result];
                    return true;
                }
                else
                {
                    Console.WriteLine("Товар с таким номером отсутствует.");
                }
            }
            else
            {
                Console.WriteLine("Неверно введен номер товара.");
            }

            return false;
        }
    }

    public class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
    }
}
