using System;
using System.Reflection;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SAMPLE APPLICATION");
            Console.WriteLine("------------------\n");
            Console.WriteLine($"Current version: {Assembly.GetExecutingAssembly().GetName().Version}");
            Console.ReadKey();
        }
    }
}
