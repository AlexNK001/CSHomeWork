using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> numbers = new Queue<int>(new[] { 1, 2, 3, 4, 5 });
            numbers.Peek();
            numbers.Dequeue();
            Console.WriteLine(numbers.Dequeue());
        }
    }
}