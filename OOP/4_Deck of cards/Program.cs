using System;
using System.Collections.Generic;

namespace _4_Deck_of_cards
{
    public class Program
    {
        static void Main()
        {
            Player player = new Player();

            Croupier crouper = new Croupier(player);
            crouper.Work();
        }
    }

    public class Croupier
    {
        private const string CommandTakeFixedCards = "1";
        private const string CommandTakeNumberCards = "2";
        private const string CommandShowPlayerCards = "3";
        private const string CommandExit = "4";

        private ConsoleColor _errorColor = ConsoleColor.Red;
        private ConsoleColor _activeColor = ConsoleColor.Green;
        private ConsoleColor _inactiveColor = ConsoleColor.DarkGray;

        private Player _player;
        private Deck _deck;
        private int _numberCards = 3;

        public Croupier(Player player)
        {
            _player = player;
            _deck = new Deck();
        }

        public void Work()
        {
            bool inGame = true;

            while (inGame)
            {
                Console.Clear();

                ShowCountCards(_player.CountCards, _deck.CountCards);
                ShowCommand(_numberCards, _deck.HaveCards, _player.HaveCards);

                string userInput = Console.ReadLine();

                if (userInput == CommandTakeFixedCards && _deck.HaveCards)
                {
                    TakeFixedAmount();
                }
                else if (userInput == CommandTakeNumberCards && _deck.HaveCards)
                {
                    TakeSelectedAmount();
                }
                else if (userInput == CommandShowPlayerCards && _player.HaveCards)
                {
                    _player.ShowCards();
                    Console.ReadKey();
                }
                else if (userInput == CommandExit)
                {
                    inGame = false;
                }
            }
        }

        private void TakeFixedAmount()
        {
            if (_deck.TryGiveCards(out List<Card> cards, _numberCards))
            {
                _player.TakeCards(cards);
            }
        }

        private void TakeSelectedAmount()
        {
            if (TryChooseCards(out int result))
            {
                if (_deck.TryGiveCards(out List<Card> cards, result))
                {
                    _player.TakeCards(cards);
                }
            }
        }

        private void ShowCountCards(int countPlayerCards, int countDeckCards)
        {
            if (countPlayerCards > 0)
            {
                WriteColorLine($"Количество карт игрока: {countPlayerCards}.", _activeColor);
            }
            else
            {
                WriteColorLine("У игрока нет карт.", _inactiveColor);
            }

            if (countDeckCards > 0)
            {
                WriteColorLine($"Количество карт в колоде: {countDeckCards}.\n", _activeColor);
            }
            else
            {
                WriteColorLine("В колоде нет карт.", _inactiveColor);
            }
        }

        private void ShowCommand(int numberCards, bool haveDeckCards, bool havePlayerCards)
        {
            if (haveDeckCards)
            {
                WriteColorLine($"{CommandTakeFixedCards} - Взять {numberCards} карты.", _activeColor);
            }
            else
            {
                WriteColorLine($"{CommandTakeFixedCards} - В колоде нет карт, брать нечего.", _inactiveColor);
            }

            WriteColorLine($"{CommandTakeNumberCards} - Указать количество желаймых карт.", _activeColor);

            if (havePlayerCards)
            {
                WriteColorLine($"{CommandShowPlayerCards} - Показать карты игрока.", _activeColor);
            }
            else
            {
                WriteColorLine($"{CommandShowPlayerCards} - У игрока пока нет карт.", _inactiveColor);
            }

            WriteColorLine($"{CommandExit} - Выход из программы.", _activeColor);
        }

        private bool TryChooseCards(out int result)
        {
            WriteColorLine("Введите количество карт, которое хотите взять.", _activeColor);

            if (int.TryParse(Console.ReadLine(), out result))
            {
                return true;
            }
            else
            {
                WriteColorLine("Неверно указана команда.", _errorColor);
                Console.ReadKey();
                return false;
            }
        }

        private void WriteColorLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public class Player
    {
        private List<Card> _cards;

        public Player()
        {
            _cards = new List<Card>();
        }

        public int CountCards => _cards.Count;
        public bool HaveCards => _cards.Count > 0;

        public void TakeCards(List<Card> cards)
        {
            if (cards == null)
            {
                Console.WriteLine("Предупреждение! Игроку передают карты = null !");
            }
            else
            {
                _cards.AddRange(cards);
            }
        }

        public void ShowCards()
        {
            foreach (Card card in _cards)
                card.ShowInfo();
        }
    }

    public class Deck
    {
        private List<Card> _cards;


        public Deck()
        {
            _cards = CreateCards();
            ShuffleCards();
        }

        public int CountCards => _cards.Count;
        public bool HaveCards => _cards.Count > 0;

        public bool TryGiveCards(out List<Card> card, int count)
        {
            if (CheakCardEntry(count))
            {
                card = _cards.GetRange(0, count);
                _cards.RemoveRange(0, count);

                return true;
            }
            else
            {
                card = null;

                Console.WriteLine("В колоде нет такого количества карт.");
                Console.ReadKey();

                return false;
            }
        }

        private bool CheakCardEntry(int number)
        {
            return number > 0 && number <= _cards.Count;
        }

        private List<Card> CreateCards()
        {
            string[] ranks = { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };
            string[] suits = { "Бубей", "Червей", "Крестей", "Пики" };

            List<Card> cards = new List<Card>();

            for (int i = 0; i < ranks.Length; i++)
            {
                for (int j = 0; j < suits.Length; j++)
                {
                    cards.Add(new Card(ranks[i], suits[j]));
                }
            }

            return cards;
        }

        private void ShuffleCards()
        {
            Random _random = new Random();

            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndex = _random.Next(_cards.Count);

                Card tempCard = _cards[i];

                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = tempCard;
            }
        }
    }

    public class Card
    {
        private string _rank;
        private string _suit;

        public Card(string rank, string suit)
        {
            _rank = rank;
            _suit = suit;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{_rank.PadLeft(8)} {_suit}");
        }
    }
}
