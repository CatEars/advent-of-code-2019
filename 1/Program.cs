using System;
using System.IO;
using System.Linq;

namespace _1
{
    class Program
    {
        private const string REAL_INPUT = "./in.real";
        private const string TEST_INPUT = "./in.tst";
        
        static void Main(string[] args)
        {
            var inputFile = REAL_INPUT;
            var lines = File.ReadLines(inputFile);
            var asNumbers = lines.Select(Int32.Parse);
            var solved = asNumbers.Select(F2);
            var result = solved.Sum();
            Console.WriteLine(result);
        }

        static int F(int x) 
        {
            return Math.Max(((int) Math.Floor(x / 3.0)) - 2, 0);
        }

        static int F2(int x)
        {
            if (x <= 0) return 0;
            var val = F(x);
            return val + F2(val);
        }
    }
}
