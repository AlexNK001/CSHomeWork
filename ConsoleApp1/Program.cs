using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class Program
	{
		static void Main(string[] args)
		{
			Book book = new Book(2, 2);
		}
	}

	public class Book
	{
		public Book(int yearRelease, int index, string name = "Senya")
		{
			YearRelease = yearRelease;
			Index = index;
			Name = name;
			Console.WriteLine("1");
		}
		public Book(string author, int index, string name = "Senya")
		{
			Author = author;
			Index = index;
			Name = name;
			Console.WriteLine("2");
		}
		public Book(int yearRelease, int index)
		{
			YearRelease = yearRelease;
			Index = index;
			Console.WriteLine("3");
		}
		public string Author { get; }
		public int YearRelease { get; }
		internal int Index { get; }
		protected string Name { get; }
	}
}
