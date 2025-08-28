using System;

namespace unimain.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("Arabic Ligature Processing Demo");
            Console.WriteLine("==============================");
            
            // Run the demonstration
            LigatureUsageExample.DemonstrateLigatureUsage();
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
