using System;
using System.Collections.Generic;

namespace _9_Supermarket
{
    public class Lesson_9
    {
        static void Main()
        {
            ClientsCreator clientsCreator = new ClientsCreator();
            Queue<Client> clients = clientsCreator.GetClients();
            SuperMarket superMarket = new SuperMarket (clients);
            superMarket.ServeClients();
            Console.ReadKey();
        }
    }

    public class SuperMarket
    {
        private readonly Queue<Client> _clients;
        private readonly List<Product> _products;
        private int _money;

        public SuperMarket(Queue<Client> clients)
        {
            _clients = clients;
            _products = GetProducts();
            _money = 0;
        }

        public void ServeClients()
        {
            const string CommandServeClient = "1";
            const string CommandExit = "2";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Денег у магазина: {_money}");
                Console.WriteLine($"Клиентов в зале: {_clients.Count}");

                if (_clients.Count == 0)
                {
                    Console.WriteLine("Клиентов нет. Магазин закрывается.");
                    isWork = false;
                }
                else
                {
                    Console.WriteLine($"{CommandServeClient} - Продать товары покупателю.");
                    Console.WriteLine($"{CommandExit} - Выход из программы.\n");

                    string userInput = Console.ReadLine();

                    if (userInput == CommandServeClient)
                    {
                        Basket basket = FillProducts();
                        basket.ShowProducts();

                        Client client = _clients.Dequeue();

                        SellProducts(client, basket);
                    }
                    else if (userInput == CommandExit)
                    {
                        isWork = false;
                    }
                    else
                    {
                        Console.WriteLine("Неверная команда.");
                    }
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void SellProducts(Client client, Basket basket)
        {
            bool isWork = true;

            while (isWork)
            {
                if (client.TryEnoughMoney(basket.GetAmount()))
                {
                    client.BuyProducts(basket.GetProducts(), basket.GetAmount());
                    _money += basket.GetAmount();
                    isWork = false;
                }
                else if (basket.HaveProducts == false)
                {
                    isWork = false;
                }
                else
                {
                    basket.DeleteRandomProduct();
                }
            }
        }

        private Basket FillProducts()
        {
            List<Product> products = new List<Product>();
            Basket basket = new Basket(products);
            int maxRandom = 3;
            int decisionNumber = 0;

            for (int i = 0; i < _products.Count; i++)
            {
                if (UserUtils.GenerateRandomNumber(maxRandom) == decisionNumber)
                    products.Add(_products[i]);
            }

            return basket;
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>()
            {
                new Product("Картошка", 10),
                new Product("Яблоко", 15),
                new Product("Груша", 17),
                new Product("Банан", 13),
                new Product("Свекла", 5),
                new Product("Лук", 4),
                new Product("Вишня", 19),
                new Product("Сыр", 17),
                new Product("Колбаса", 11),
                new Product("Хлеб", 5),
                new Product("Молоко", 7),
                new Product("Чеснок", 2),
                new Product("Уксус", 3),
                new Product("Соль", 1),
            };

            return products;
        }
    }

    public class Client
    {
        private List<Product> _products;
        private int _money;

        public Client(int money)
        {
            _products = new List<Product>();
            _money = money;
        }

        public bool TryEnoughMoney(int sum)
        {
            return _money >= sum;
        }

        public void BuyProducts(List<Product> products, int sum)
        {
            _products.AddRange(products);
            _money -= sum;
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

        public Product Clone()
        {
            return new Product(Name, Price);
        }
    }

    public class Basket
    {
        private List<Product> _products;

        public Basket(List<Product> products)
        {
            _products = products;
        }

        public bool HaveProducts => _products.Count > 0;

        public void DeleteRandomProduct()
        {
            if (HaveProducts)
            {
                int index = UserUtils.GenerateRandomNumber(_products.Count);

                Console.WriteLine($"Удалён продукт {_products[index].Name}");

                _products.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("В тележке нет продуктов для удаления.");
            }
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            foreach (Product product in _products)
            {
                products.Add(product.Clone());
            }

            return products;
        }

        public int GetAmount()
        {
            int sum = 0;

            foreach (Product product in _products)
            {
                sum += product.Price;
            }

            return sum;
        }

        public void ShowProducts()
        {
            if (HaveProducts)
            {
                foreach (Product product in _products)
                    Console.WriteLine($"Товар: {product.Name} цена: {product.Price}.");
            }
            else
            {
                Console.WriteLine("Корзина пуста.");
            }
        }
    }

    public class ClientsCreator
    {
        public Queue<Client> GetClients()
        {
            Queue<Client> clients = new Queue<Client>();
            int countClient = 10;

            for (int i = 0; i < countClient; i++)
            {
                clients.Enqueue(new Client(GetMoney()));
            }

            return clients;
        }

        private int GetMoney()
        {
            int minMoney = 30;
            int maxMoney = 150;

            return UserUtils.GenerateRandomNumber(minMoney, maxMoney);
        }
    }

    public abstract class UserUtils
    {
        static private Random s_random = new Random();

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
