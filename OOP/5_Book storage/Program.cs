using System;
using System.Collections.Generic;

namespace _5_Book_storage
{
    public class Program
    {
        private static void Main()
        {
            const string CommandAddBook = "1";
            const string CommandDeleteBook = "2";
            const string CommandShowBooks = "3";
            const string CommandShowBooksByName = "4";
            const string CommandShowBooksByAuthors = "5";
            const string CommandShowBooksByAge = "6";
            const string CommandExit = "7";

            bool isWork = true;

            Library books = new Library(new List<Book> {
            new Book("Фёдор Достоевский","Преступление и наказание", 1866),
            new Book("Фёдор Достоевский","Братья Карамазовы", 1880),
            new Book("Фёдор Достоевский","Бесы", 1872),
            new Book("Фёдор Достоевский","Записки из подполья", 1864),

            new Book("Александр Пушкин","Медный всадник", 1837),
            new Book("Александр Пушкин","Пиковая дама", 1834),
            new Book("Александр Пушкин","Капитанская дочка", 1836),
            new Book("Александр Пушкин","Руслан и Людмила", 1820),

            new Book("Михаил Лермонтов","Герой нашего времени", 1840),
            new Book("Михаил Лермонтов","Парус", 1841),
            new Book("Михаил Лермонтов","Смерть поэта", 1837),
            new Book("Михаил Лермонтов","Бородино", 1837)});

            while (isWork)
            {
                Console.Clear();

                Console.WriteLine($"{CommandAddBook} - Добавить книгу.");
                Console.WriteLine($"{CommandDeleteBook} - Убрать книгу.");
                Console.WriteLine($"{CommandShowBooks} - Показать все книги.");
                Console.WriteLine($"{CommandShowBooksByName} - Показать все книги по названию.");
                Console.WriteLine($"{CommandShowBooksByAuthors} - Показать все книги по автору.");
                Console.WriteLine($"{CommandShowBooksByAge} - Показать все книги по году выпуска.");
                Console.WriteLine($"{CommandExit} - Выход.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddBook:
                        books.AddBook();
                        break;

                    case CommandDeleteBook:
                        books.DeleteBook();
                        break;

                    case CommandShowBooks:
                        books.ShowBooks();
                        break;

                    case CommandShowBooksByName:
                        books.ShowByTitle();
                        break;

                    case CommandShowBooksByAuthors:
                        books.ShowByAuthor();
                        break;

                    case CommandShowBooksByAge:
                        books.ShowByPublicationDate();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    public class Book
    {
        public Book(string authorName, string bookName, int publicationDate)
        {
            AuthorName = authorName;
            BookName = bookName;
            PublicationDate = publicationDate;
        }

        public string AuthorName { get; private set; }
        public string BookName { get; private set; }
        public int PublicationDate { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Автор: {AuthorName} Книга: {BookName} Дата издания: {PublicationDate}");
        }
    }

    public class Library
    {
        private readonly List<Book> _books;

        public Library(List<Book> books)
        {
            _books = books;
        }

        public void AddBook()
        {
            Console.WriteLine("Введите имя автора.");
            string authorName = Console.ReadLine();

            if (authorName == "")
            {
                Console.WriteLine("Имя автора должно содержать символы.");
            }
            else
            {
                Console.WriteLine("Введите название книги.");
                string bookName = Console.ReadLine();

                if (bookName == "")
                {
                    Console.WriteLine("Название книги должно содержать символы.");
                }
                else
                {
                    Console.WriteLine("Введите дату публикации.");

                    if (int.TryParse(Console.ReadLine(), out int publicationData))
                    {
                        _books.Add(new Book(authorName, bookName, publicationData));
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод даты.");
                    }
                }
            }
        }

        public void ShowBooks()
        {
            for (int i = 1; i <= _books.Count; i++)
            {
                Console.Write($"{i}_");
                _books[i - 1].ShowInfo();
            }
        }

        public void DeleteBook()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 3);
            ShowBooks();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("Введите номер книги для удаления.");

            if (int.TryParse(Console.ReadLine(), out int result))
            {
                if (result > 0 && result <= _books.Count)
                    _books.RemoveAt(result - 1);
                else
                    Console.WriteLine("Неверный номер книги.");
            }
            else
            {
                Console.Write("Введено некоретное число.");
            }
        }

        public void ShowByTitle()
        {
            Console.WriteLine("Введите название книги для поиска.");
            string wordToFind = Console.ReadLine();

            for (int i = 0; i < _books.Count; i++)
            {
                if (SearchMatches(_books[i].BookName, wordToFind))
                    _books[i].ShowInfo();
            }
        }

        public void ShowByAuthor()
        {
            Console.WriteLine("Введите имя автора для поиска.");
            string wordToFind = Console.ReadLine();

            for (int i = 0; i < _books.Count; i++)
            {
                if (SearchMatches(_books[i].AuthorName, wordToFind))
                    _books[i].ShowInfo();
            }
        }

        public void ShowByPublicationDate()
        {
            Console.WriteLine("Введите дату для поиска.");

            if (int.TryParse(Console.ReadLine(), out int numberToFind))
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    if (numberToFind == _books[i].PublicationDate)
                        _books[i].ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }

        private bool SearchMatches(string wordToSearch, string wordToFind)
        {
            wordToSearch = wordToSearch.ToLower();
            wordToFind = wordToFind.ToLower();

            return wordToSearch.Contains(wordToFind);
        }
    }
}
