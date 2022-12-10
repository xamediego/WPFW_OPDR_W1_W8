using System;
using ConsoleApplication1.Map;
using ConsoleApplication1.View;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main()
        {
            // Starter.DrawTest();
            
            while (true)
            {
                Console.WriteLine("1. Go to login\n2. Draw Test map\n3. Exit");
                string input = Console.ReadLine();
                if (input == "1") Login.MainMenu();
                if (input == "2") Starter.DrawTest();
                if (input == "3") break;
            }
        }
    }
}