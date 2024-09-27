using System;

namespace power_of_two
{
    internal class Program
    {
        private static void Main()
        {
			int mySpeed = 1000;
			int myCarSpeed = 250;
			int airPlaneSpeed = 5000;
			if (mySpeed >= myCarSpeed || mySpeed <= airPlaneSpeed && myCarSpeed < airPlaneSpeed)
			{
				Console.WriteLine("Я быстрый но самолёт быстрее");
			}
			else
			{
				Console.WriteLine("Я самый быстрый человек на планете");
			}


			//Console.WriteLine("Введите цифру:");
			//Console.WriteLine("1 - умножение");
			//Console.WriteLine("2 - деление");
			//Console.WriteLine("3 - сложение");
			//Console.WriteLine("4 - вычитание");
			//string input = Console.ReadLine();
			//int result = 10;
			//switch (input)
			//{
			//	case "1":
			//result *= 2;
			//		break;
			//	case "2":
			//result /= 2;
			//		break;
			//	case "3":
			//result += 2;
			//		break;
			//	case "4":
			//result -= 2;
			//		break;
			//	default:
			//		Console.WriteLine("Нет такой команды");
			//		break;
			//}
			//if (result % 2 == 0)
			//{
			//	int summand = 5;
			//	result += summand;
			//}
			//else
			//{
			//	int summand = 3;
			//	result += summand;
			//}
			//Console.WriteLine($"результат = {result}");
		}
    }
}
