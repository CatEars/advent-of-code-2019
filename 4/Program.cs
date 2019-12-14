using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _4
{
    class Program
    {
        private const string REAL_INPUT = "./in.real";

        private static bool TwoAdjacentSame(string x)
        {
            for (var idx = 1; idx < x.Count(); ++idx)
            {
                if (x[idx] == x[idx - 1]) return true;
            }
            return false;
        }

        private static bool IncreasingDigits(string x)
        {
            for (var idx = 1; idx < x.Count(); ++idx)
            {
                if (x[idx] < x[idx - 1]) return false;
            }
            return true;
        }

        private static bool ExactlyTwoAdjacent(string x)
        {
            var counter = new Dictionary<char, int>();
            for (var idx = 1; idx < x.Count(); ++idx)
            {
                if (x[idx] == x[idx - 1])
                {
                    counter[x[idx]] = counter.ContainsKey(x[idx]) ?
                        counter[x[idx]] + 1 : 1;
                }
            }
            return counter.Values.Any(v => v == 1);
        }

        private static bool MatchingPassword(string x)
        {
            return TwoAdjacentSame(x) && IncreasingDigits(x);
        }

        static void Main(string[] args)
        {
            var values = File.ReadAllText(REAL_INPUT);
            var split = values.Split('-');
            Console.WriteLine(values);
            Console.WriteLine(split[0]);
            Console.WriteLine(split[1]);
            var LOW = Int32.Parse(split[0]);
            var HIGH = Int32.Parse(split[1]);
            var numbers = Enumerable.Range(LOW, HIGH - LOW);
            var passwordCandidates = numbers
                .Select(y => $"{y}")
                .Where(MatchingPassword)
                .ToList();
            var secondPasswordCandidates = passwordCandidates
                .Where(ExactlyTwoAdjacent);

            var numPasswords = passwordCandidates.Count();
            var numSecondPasswords = secondPasswordCandidates.Count();
            
            Console.WriteLine($" Advent 4 - Challenge #1 = {numPasswords}");
            Console.WriteLine($" Advent 4 - Challenge #2 = {numSecondPasswords}");
        }
    }
}
