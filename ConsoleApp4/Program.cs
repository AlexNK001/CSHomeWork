using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
			int index = 2;
			List<int> collection = new List<int>()
		{
			4, 1, 2, 3
		};
			collection.Remove(4);
			InsertCollection(collection, index);
			foreach (int item in collection)
			{
				Console.Write(item);
			}
		}

		static void InsertCollection(List<int> collection, int index = 0)
		{
			collection.InsertRange(index, new[] { 5, 6, 7 });
		}
	}
}
